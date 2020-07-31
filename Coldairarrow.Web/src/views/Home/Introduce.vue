<template>
  <div id="remote-control">
    <a-tabs v-model="tabActive" @change="tabChange">
      <a-tab-pane
        v-for="item in departMentList"
        :key="item.Id"
        @mouseover="tabMouseOver"
        @mouseout="tabMouseOut"
      >
        <div slot="tab">
          <label class="tab">{{ item.Name }}</label>
        </div>
        <a-card :bordered="false">
          <div>
            <table class="border-table" style="height: 100%;">
              <tr>
                <td width="80px">开关名称</td>
                <td width="80px" v-for="item in switchList" :key="item.index">{{ item.name }}</td>
              </tr>
              <tr>
                <td>当前状态</td>
                <td v-for="item in switchList" :key="item.index">
                  <span
                    v-if="item.value == 1 || item.value == 0"
                    :class="{ switch: true, open: item.value == 1 }"
                  >{{ item.value == 1 ? '开' : '关' }}</span>
                </td>
              </tr>
              <tr>
                <td>开关控制</td>
                <td v-for="item in switchList" :key="item.index">
                  <a-switch
                    v-if="item.has"
                    checkedChildren="开"
                    unCheckedChildren="关"
                    :defaultChecked="item.value == 1"
                    @change="change"
                  />
                </td>
              </tr>
            </table>
          </div>
        </a-card>
      </a-tab-pane>
    </a-tabs>
  </div>
</template>

<script>
    import ChartCard from '@/components/ChartCard/ChartCard'
    import Trend from '@/components/ChartCard/Trend'
    import signalr from '@/utils/signalr'
    import _ from 'underscore'
    import OperatorCache from '@/utils/cache/OperatorCache'
    import TokenCache from '@/utils/cache/TokenCache'
    import FingerInput from '../../components/FingerInput'
    import FingerCheck from '../../components/FingerCheck'
    import ModuleContent from '../../components/ModuleContent'
    import Report from '../../components/Report'
    import remoteHub from '../../utils/signalr'
    import storage from '../../utils/storage'
    import moment from 'moment'

    var vm

    // setInterval(() => {
    //     var index = vm.departMentList.findIndex(item => item.Id == vm.tabActive)
    //     if (index >= vm.departMentList.length - 1) {
    //         index = -1
    //     }
    //     console.log(index)
    //     vm.tabActive = vm.departMentList[index + 1].Id
    //     vm.tabChange(vm.tabActive)
    // }, 5000)

    export default {
        components: {
            ChartCard,
            Trend,
            FingerInput,
            FingerCheck,
            ModuleContent,
            Report
        },

        data() {
            return {
                crtModule: { visible: false, title: 'sfe', index: -1 },
                tabActive: '',
                datas: [],
                allModuls: [],
                data: [],
                DeviceModuledata: [],
                departMentList: [],
                dataStruct: [],
                userList: [],
                meterReadingBtn: true,
                currentDate: new Date(),
                onlineCount: 3,
                currentDept: null,
                isShowReport: false,
                tem: '',
                humidity: ''
            }
        },

        mounted() {
            vm = this
            var p1 = this.$http.get('/Home/Index/DepartmentList')
            //var p2 = this.$http.get('/Home/Index/ModulesList')

            Promise.all([p1]).then(res => {
                let res1 = res[0]
                //let res2 = res[1]

                if (res1.Data.length > 0) this.tabActive = res1.Data[0].Id
                else this.tabActive = ''
                this.tabChange()
                this.departMentList = res1.Data.map(item => {
                    item.onlineCount = 3
                    return item
                })

                //this.allModuls = res2.Data
            })

            var canvas = this.$refs.canvas

            //获取人员信息
            this.$http.get('/Base_Manage/Base_User/GetUserByTeamIdDataList').then(res => {
                this.userList = res.Data
            })

            //获取视频数据
            this.$http.get('/Hkv/T_Video/GetDataAll').then(res => {
                for (let i = 0; i < canvas.length; i++) {
                    if (i <= res.Data.length - 1) {
                        let url = res.Data[i].RtspUrl
                        let player = new JSMpeg.Player(url, {
                            canvas: canvas[i]
                        })
                    }
                }
            })

            //定时器
            setInterval(() => {
                this.departMentList.forEach(item => {
                    if (item.onlineCount > 0) {
                        item.onlineCount--
                    }
                })
            }, 1000)

            let _self = this
            //数据推送
            remoteHub.acceptMessage('DeviceData', (datas, moduleId) => {
                if (this.tabActive == moduleId) {
                    let newDatas = []
                    for (let index = 0; index < datas.length; index++) {
                        var model = {}
                        for (let name in datas[index]) {
                            model[name.substr(0, 1).toUpperCase() + name.substr(1)] = datas[index][name]
                        }
                        newDatas.push(model)
                    }

                    this.datas = newDatas
                }

                _self.departMentList.forEach(item => {
                    if (item.Id == moduleId) {
                        item.onlineCount = _self.onlineCount
                    }
                })
            })

            //定时获取抄表
            remoteHub.acceptMessage('MeterReading', data => {
                this.meterReadingBtn = data.disabled
                if (data.disabled) {
                    storage.removeLocal('btns')
                }

                this.currentDate = new Date(data.datetime)
            })

            //设备属性变更
            remoteHub.acceptMessage('DeviceStatusChange', data => {
                let datas = this.dataStruct.filter(item => item.DeviceId == data.DeviceId)
                datas.DeviceStatus = data.Status
            })

            this.$http.get('/Home/Index/GetWeather', { params: { appid: '85866456', appsecret: 'tB8eMbDH' } }).then(res => {
                this.tem = res.tem
                this.humidity = res.humidity
            })
        },

        methods: {
            //交接班
            handoverClick() {
                this.isShowReport = true
            },

            reportSubmit() {
                this.$refs.fingerCheck.isShow = true
            },

            inputCompleted() {},

            fullVideo(e) {
                e.target.requestFullscreen()
            },

            handleLogout() {
                const that = this

                this.$confirm({
                    title: '提示',
                    content: '真的要注销登录吗 ?',
                    onOk() {
                        TokenCache.deleteToken()
                        OperatorCache.clear()
                        location.reload()
                        // that.$router.push({ path: '/user/login' })
                    }
                })
            },

            handleMenuClick(menu) {
                if (menu.key == 'fullscreen') {
                    if (document.fullscreenElement) document.exitFullscreen()
                    else document.documentElement.requestFullscreen()
                } else if (menu.key == 'setting') {
                    window.open('/Home/Introduce', '_blank')
                } else if (menu.key == 'video') {
                    console.log(33)
                } else if (menu.key == 'logout') {
                    this.$confirm({ title: '系统消息', content: '确定退出系统么？' }).then(() => {
                        this.handleLogout()
                    })
                }
            },

            tabChange(key) {
                console.log(this.tabActive)
                this.$http
                    .get('/Home/Index/GetDataList', {
                        params: {
                            departmentId: this.tabActive
                        }
                    })
                    .then(res => {
                        this.dataStruct = res.Data
                        this.loadData(this.tabActive)
                    })

                this.$http.get('/Home/Index/ModulesList', { params: { departmentId: this.tabActive } }).then(res => {
                    this.allModuls = res.data
                })
            },

            //充电放电
            electric(type) {
                var _self = this
                this.$http.post('/Home/Index/Electric', { type: type }).then(res => {
                    if (res.Success) {
                        _self.$message.success('指令已发送')
                    } else {
                        _self.$message.error(res.Message)
                    }
                })
            },

            tabMouseOver() {},

            tabMouseOut() {},

            getBattyArr(item) {
                var arr = []
                for (let i = 0; i < 24; i++) {
                    arr[i] = { index: i }
                }
                var props = (item.deviceList || [])[0].propList || []
                var prop = props.find(p => p.propName == 'voltage') //"voltage"
                if (prop) {
                    var value = prop.value || ''
                    if (value.indexOf('[') > -1) {
                        value = value.replace('[', '')
                    }
                    if (value.indexOf(']') > -1) {
                        value = value.replace(']', '')
                    }
                    _.each(value.split(','), (tmp, index) => {
                        arr[index].value = tmp
                    })
                }

                return arr
            },

            butCick(index, model) {
                var mid
                var people = 1 // 1人工抄表 0自动抄表
                var timesetupid = '1241755128407527424' //抄表时间 Id
                var confirmuserid = '1242376184319184896' //指纹确认Id
                if (index == 1) {
                    mid = model.moduleId
                }

                var data = JSON.stringify(model)

                this.$http
                    .post('/MeterReaDing/MeterReaDingOnDuty/SaveData', {
                        jsondata: data,
                        mid: mid,
                        people: people,
                        timesetupid: timesetupid,
                        confirmuserid: confirmuserid
                    })
                    .then(res => {
                        if (res.Success == true) {
                            alert('抄表完成')
                        }
                    })
            },

            moduleClick(module, index) {
                this.crtModule.visible = true
                this.crtModule.index = index
                this.crtModule.title = module.moduleName
            },

            loadData(departmentId) {
                this.$http
                    .get('/Home/Index/GetDeviceDataByDepartmentId', {
                        params: { departmentId: departmentId }
                    })
                    .then(res => {
                        this.datas = res.Data
                    })
            },

            policeBattery(item, value) {
                var props = (item.deviceList || [])[0].propList || []
                var prop = props.find(p => p.propName == 'voltage') //"voltage"

                var min_d = prop.min
                var max_d = prop.max
                var value_d = parseFloat(value)
                if (min_d) {
                    if (value_d < min_d) return true
                }
                if (max_d) {
                    if (value_d > max_d) return true
                }
            },

            dateFormat(date, format) {
                return moment(date).format(format)
            },

            change() {
                const _self = this
                this.$http.post('/Home/Index/RemoveControl', { deviceId: 1 }).then(res => {
                    if (res.Success) {
                        _self.$message.success('指令已发送')
                    } else {
                        _self.$message.error(res.Message)
                    }
                })
            }
        },

        computed: {
            modules() {
                let _self = this
                var datas = []
                var moduleObj = _.groupBy(this.dataStruct, 'DeviceDisplayModuleId')
                _.each(moduleObj, (module, moduleId) => {
                    var data1 = {
                        moduleId: moduleId,
                        moduleName: module[0].DeviceDisplayModuleName,
                        deviceTypeName: module[0].DeviceTypeName,
                        deviceTypeId: module[0].DeviceTypeId,
                        position: module[0].Position,
                        deviceList: []
                    }
                    datas.push(data1)
                    var deviceObj = _.groupBy(module, 'Deviceid')
                    _.each(deviceObj, (device, deviceId) => {
                        let data2 = {
                            deviceId: deviceId,
                            deviceName: device[0].DeviceName,
                            deviceTypeName: device[0].DeviceTypeName,
                            deviceStatus: device[0].DeviceStatus,
                            propList: []
                        }

                        //设置设备的代理
                        data2 = new Proxy(data2, {
                            get(target, key) {
                                if (['deviceId', 'deviceName', 'propList'].indexOf(key) >= 0) {
                                    return target[key]
                                }

                                if (key == 'length') {
                                    return target.propList.length
                                }

                                if (key == 'firstProp') {
                                    return target.propList[0]
                                }

                                var prop = target.propList.find(x => x.propName == key)
                                if (!prop) {
                                    prop = {}
                                }

                                return prop
                            }
                        })

                        data1['deviceList'].push(data2)
                        var propObj = _.groupBy(device, 'DevicePropId')
                        _.each(propObj, (prop, propId) => {
                            let obj = this.datas.find(x => x.DeviceId == deviceId && x.DevicePropId == propId)

                            if (!obj) obj = {}

                            let data3 = {
                                propId: propId,
                                propName: prop[0].DevicePropName,
                                displayName: prop[0].DevicePropDisplayName,
                                propIsShow: prop[0].DevicePropIsShow,
                                propUnit: prop[0].DevicePropUnit,
                                max: prop[0].Max,
                                min: prop[0].Min,
                                propType: prop[0].PropType,
                                value: obj.Value,
                                date: obj.CreateTime
                            }

                            data2['propList'].push(data3)
                        })
                    })
                })

                this.allModuls.forEach(item => {
                    if (!datas.find(x => x.moduleId == item.deviceDisplayModuleId)) {
                        datas.push({
                            moduleId: item.id,
                            moduleName: item.deviceDisplayModuleName,
                            position: item.position
                        })
                    }
                })

                console.table(JSON.parse(JSON.stringify(this.datas)))
                console.log('dddddd')
                console.log(datas)
                return datas
            },

            modules1() {
                var data = this.modules.filter(item => item.position == 1)
                return data
            },

            modules2() {
                var data = this.modules.filter(item => item.position == 2)

                return data
            },

            modules3() {
                var data = this.modules.filter(item => item.position == 3)
                return data
            },

            switchList() {
                var array = []
                for (let index = 0; index < 16; index++) {
                    array[index] = {
                        index: index,
                        name: index,
                        has: false
                    }
                }

                if (this.modules3.length > 0) {
                    _.each(this.modules3[0].deviceList, (item, index) => {
                        var props = item.propList || []
                        if (props.length > 0) {
                            array[index].name = item.deviceName
                            array[index].value = props[0].value
                            array[index].device = item
                            array[index].has = true
                        }
                    })
                }

                return array
            },

            btnDisabled() {
                return this.meterReadingBtn
            }
        }
    }
</script>
<style>
    .show-module .round {
        width: 472px;
        height: 472px;
    }

    .show-module .round .img {
        width: 410px;
        height: 410px;
        border-radius: 205px;
    }

    .show-module .device {
        font-size: 20px;
    }

    .show-module .field {
        font-size: 15px !important;
        margin-top: 5px;
    }

    .show-module .name {
        display: inline-block;
        vertical-align: top;
    }

    .show-module .three-electric tbody tr {
        height: 40px;
    }
</style>
<style scoped>
    @media screen and (min-width: 576px) and (max-width: 768px) {
        .panel:nth-child(n + 1) {
            margin-top: 20px;
        }
    }

    @media screen and (min-width: 768px) and (max-width: 1200px) {
        .panel:nth-child(n + 3) {
            margin-top: 20px;
        }
    }

    @media screen and (min-width: 1200px) {
        .panel:nth-child(n + 5) {
            margin-top: 20px;
        }
    }

    .tab {
        cursor: pointer;
    }

    .module {
        cursor: pointer;
    }

    .current-date {
        font-size: 22px;
        width: 100%;
        text-align: center;
        bottom: 0;
        position: absolute;
    }

    .current-date .date {
        margin-bottom: 20px;
    }

    .current-date .date,
    .current-date .time {
        display: block;
    }

    .full-height {
        position: relative;
        height: 100%;
    }

    .main-content {
        padding: 0 10px;
    }

    .video-box {
        overflow-x: auto;
        display: inline-block;
        white-space: nowrap;
        width: 100%;
    }

    .video-box::after {
        content: '';
        display: block;
        position: relative;
        clear: both;
    }
    .video-box .video {
        display: inline-block;
        background-color: black;
        cursor: pointer;
    }

    .video {
        width: 290px;
        height: 170px;
        border: 1px solid gray;
    }

    .video + .video {
        margin-left: 10px;
    }

    .video canvas {
        width: 100%;
        height: 100%;
    }

    table.text-center th,
    table.text-center td {
        text-align: center;
    }

    .logo > img {
        width: 30px;
        position: absolute;
        left: 10px;
    }

    html,
    body {
        overflow-x: hidden !important;
    }

    .title-box {
        position: relative;
        padding: 5px 0;
        background-color: #000000;
    }

    .custom-dropdown {
        background-color: blue;
    }

    .title-setting {
        position: absolute;
        top: 0px;
        right: 7px;
    }

    .main-title {
        margin: auto;
        text-align: center;
        font-size: 22px;
        color: #f2b428;
        font-weight: 800;
    }

    .panel-card {
        height: 250px;
    }

    .cell {
        width: 50px;
    }

    .nothing {
        height: 187px;
        display: inline-block;
        text-align: center;
        width: 100%;
        vertical-align: middle;
        line-height: 187px;
    }

    .panel-title {
        margin: 0 20px;
        font-size: 20px;
    }

    .switch-img {
        height: 50px;
        width: 50px;
    }

    button.big {
        display: block;
        margin: 10px auto;
        padding: 8px 60px;
        width: auto;
        height: auto;
    }

    .tbox button {
        display: block;
        height: 40px;
        margin: 6px;
        background-color: #1890ff;
    }

    .h100p {
        height: 100% !important;
    }

    .line-box {
        margin-bottom: 8px;
    }

    .btn-vertical {
        width: 45px;
        height: 100%;
    }

    .btn-vertical span {
        writing-mode: tb-rl !important;
    }

    .imgdiv {
        width: 100% !important;
    }

    .imgdiv::after {
        content: '';
        display: block;
        position: relative;
        clear: both;
    }

    .clear ::after {
        content: '';
        display: block;
        position: relative;
        clear: both;
    }

    .imgdiv > img {
        width: 125px;
        height: 125px;
        float: left;
    }

    .imgdiv > div {
        float: left;
        width: 5%;
    }

    .imgdiv > button {
        float: left;
        width: 45px;
        height: 125px !important;
        top: 0px !important;
    }

    table {
        width: 100%;
    }

    .border-table td,
    .border-table th {
        border: 1px solid #2ba9c6;
        text-align: center;
    }

    .setTitle {
        float: left;
        width: 11% !important;
    }

    .dutyButton {
        text-align: center;
    }

    .dutyButton > button {
        width: 145px;
        height: 45px;
        top: 20px;
    }

    .vert {
        width: 20px;
        white-space: normal;
    }

    .vert span {
        white-space: inherit;
    }

    .switch.open {
        background-color: red;
    }

    .switch {
        border: 1px solid black;
        width: 48px;
        display: inline-block;
        border-radius: 11px;
        background-color: gray;
    }

    .red {
        color: red;
    }

    .modal * {
        color: black !important;
    }

    .user-list {
        text-align: center;
        height: 100px;
        display: table;
        width: 100%;
    }

    .user-list > div {
        vertical-align: middle;
        display: table-cell;
        height: 100px;
    }

    .user-list span {
        margin: 0 5px;
        font-size: 20px;
    }

    .h500 {
        height: 500px;
    }

    .show-module .three-electric td {
        height: 30px;
    }
    .h6-title {
        color: #f2b428;
        margin: auto;
        width: 490px;
        text-align: right;
        margin-top: -24px;
    }
    .date-title {
        position: absolute;
        top: 0;
    }
    .title-date {
        margin-left: 20px;
        color: red;
        font-weight: 800;
        font-size: 22px;
    }
    .title-span-data {
        position: absolute;
        margin-left: 260px;
        margin-top: -30px;
        color: red;
    }

    .badge {
        position: absolute;
        right: 0;
    }

    .panel[data-v-d5e59f88]:nth-child(n + 1) {
        margin-top: 10px !important;
    }

    .sm-btn {
        font-size: 10px;
        width: 60px;
    }

    .led-time {
        font-size: 28px;
        color: red;
    }
    .tt {
        font-family: 'led regular';
        margin-left: 10px;
        font-size: 22px;
        color: gray;
    }
 
</style>


<style lang="less" scoped>
    table {
        tr {
            height: 30px;
        }

        td {
            border: 1px solid gray;
            text-align: center;
        }
    }
</style>
