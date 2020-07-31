<template>
  <div id="main-page" v-cloak>
    <div class="title-box">
      <div class="logo">
        <div class="date-title">
          <span class="date title-date led-time">
            {{
            dateFormat(currentDate, 'YYYY 年 MM 月 DD 日 dddd')
            }}
          </span>
        </div>
      </div>
      <div class="main-title">内蒙古广播电视台＊ 智慧机房值班管控系统</div>
      <div class="title-setting">
        <span class="time title-date led-time">{{ dateFormat(currentDate, 'HH:mm:ss') }}</span>
        <span class="tt tem">温度：{{ tem }} ℃</span>
        <span class="tt humidity">湿度 {{ humidity }}</span>
      </div>
    </div>
    <a-tabs v-model="tabActive" @change="tabChange">
      <a-tab-pane
        v-for="item in departMentList"
        :key="item.Id"
        @mouseover="tabMouseOver"
        @mouseout="tabMouseOut"
      >
        <div slot="tab">
          <label class="tab">
            {{ item.Name }}
            <a-badge class="badge" :status="item.onlineCount > 0 ? 'success' : 'error'" />
          </label>
        </div>
        <div class="main-content">
          <div class="line-box" v-if="modules1">
            <a-row :gutter="24">
              <a-col
                class="panel"
                :sm="24"
                :md="12"
                :xl="6"
                v-for="(mod, index) in modules1"
                :key="mod.moduleId"
                style="padding:0"
              >
                <a-card class="ant-card-small panel-card my-card" size="small">
                  <div slot="title">
                    <label class="module" @click="moduleClick(mod, index)">{{ mod.moduleName }}</label>
                  </div>
                  <div slot="extra">
                    <a-button
                      @click="butClick(1, mod)"
                      size="small"
                      class="my-btn"
                      :disabled="getBtnDisabled(mod.moduleId)"
                      type="primary"
                    >抄表</a-button>
                  </div>
                  <module-content :module="mod" @wanring="baojing" @stop="sotp"></module-content>
                </a-card>
              </a-col>
              <a-col class="panel" :sm="24" :md="12" :xl="6" style="padding:0">
                <a-card class="panel-card ant-card-small my-card" size="small" title="今日值班">
                  <div class="full-height">
                    <div style="max-height:200px;overflow-y:auto">
                      <table class="border-table">
                        <tr>
                          <th>序号</th>
                          <th>值班人员</th>
                          <th>员工编号</th>
                          <th>值班情况</th>
                        </tr>
                        <tr v-for="(user, index) in userList" :key="user.Id">
                          <th>{{ index + 1 }}</th>
                          <th>{{ user.RealName }}</th>
                          <th>11011</th>
                          <th>在岗</th>
                        </tr>
                      </table>
                    </div>
                    <table class="table-btns">
                      <tr>
                        <td>
                          <a-button
                            class="sm-btn"
                            size="small"
                            type="primary"
                            @click="handleMenuClick({ key: '' })"
                          >故障上报</a-button>
                        </td>
                        <td>
                          <a-button
                            class="sm-btn"
                            size="small"
                            type="primary"
                            @click="handleMenuClick({ key: 'setting' })"
                          >后台管理</a-button>
                        </td>
                        <td>
                          <a-button
                            class="sm-btn"
                            size="small"
                            type="primary"
                            @click="handleMenuClick({ key: '' })"
                          >紧急通话</a-button>
                        </td>
                        <td>
                          <a-button
                            class="sm-btn"
                            size="small"
                            type="primary"
                            @click="handleMenuClick({ key: 'fullscreen' })"
                          >全屏/退出</a-button>
                        </td>
                        <td rowspan="2" class="btn-big">
                          <a-button type="primary" @click="handoverClick">交接班</a-button>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <a-button
                            class="sm-btn"
                            size="small"
                            type="primary"
                            @click="handleMenuClick({ key: 'fullscreen' })"
                          >日志查询</a-button>
                        </td>
                        <td>
                          <a-button
                            class="sm-btn"
                            size="small"
                            type="primary"
                            @click="handleMenuClick({ key: 'fullscreen' })"
                          >报警查询</a-button>
                        </td>
                        <td>
                          <a-button
                            class="sm-btn"
                            size="small"
                            type="primary"
                            @click="handleMenuClick({ key: 'hkv' })"
                          >全部视频</a-button>
                        </td>
                        <td>
                          <a-button
                            class="sm-btn"
                            size="small"
                            type="primary"
                            @click="handleMenuClick({ key: 'logout' })"
                          >退出</a-button>
                        </td>
                      </tr>
                    </table>

                    <!-- <div class="user-list">
                      <div>
                        <span v-for="user in userList" :key="user.Id">{{ user.RealName }}</span>
                      </div>
                    </div>-->
                    <!-- <div class="current-date">
                                            <span class="date">{{
                                                dateFormat(currentDate, 'YYYY年MM月DD日 dddd')
                                            }}</span>
                                            <span class="time">{{ dateFormat(currentDate, 'HH:mm:ss') }}</span>
                    </div>-->
                  </div>
                </a-card>
              </a-col>
            </a-row>
          </div>
          <div class="line-box outer-box">
            <div class="table-box" v-if="modules2">
              <table class="border-table" style="height:90px">
                <colgroup width="auto"></colgroup>
                <colgroup width="70"></colgroup>
                <colgroup width="70"></colgroup>
                <colgroup width="70"></colgroup>
                <tr>
                  <td rowspan="3" style="width:40px">
                    <!-- <a-button type="primary" class="btn-vertical">电池检测</a-button> -->
                    <a-button
                      type="primary"
                      class="btn-vertical"
                      @click="moduleClick(modules2, 3)"
                    >{{ modules2.moduleName }}</a-button>
                  </td>
                  <td rowspan="3">
                    <a-button type="primary" @click="electric('+')" style="margin-bottom:6px">充电</a-button>
                    <a-button type="primary" @click="electric('-')">放电</a-button>
                  </td>
                  <td>蓄电池</td>
                  <td>总电压</td>
                  <td class="cell" v-for="index in 24" :key="index">{{ index }}#</td>
                  <td rowspan="3" width="114px">
                    <a-button
                      type="primary"
                      class="my-btn"
                      :disabled="getBtnDisabled(modules2.moduleId)"
                      @click="butClick(2, modules2)"
                    >抄表</a-button>
                  </td>
                </tr>
                <tr>
                  <td>+48</td>
                  <td>+{{ (modules2.deviceList[0].voltageTotal || {}).value }}</td>
                  <td
                    v-for="data in getBattyArr(modules2.deviceList[0])"
                    :class="{ red: policeBattery(modules2.deviceList[0], data.value) }"
                    :key="data.index"
                  >{{ data.value }}</td>
                </tr>
                <tr>
                  <td>-48</td>
                  <td>-{{ (modules2.deviceList[1].voltageTotal || {}).value }}</td>
                  <td
                    v-for="data in getBattyArr(modules2.deviceList[1])"
                    :class="{ red: policeBattery(modules2.deviceList[1], data.value) }"
                    :key="data.index"
                  >-{{ data.value }}</td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </a-tab-pane>
    </a-tabs>
    <div class="main-content">
      <div class="line-box outer-box" style="margin-right: -12px;">
        <div class="video-box">
          <div class="video" v-for="(item, index) in 16" :key="index">
            <canvas ref="canvas" @dblclick="fullVideo" :data-index="item"></canvas>
          </div>
        </div>
      </div>
    </div>

    <!-- <a-modal
      v-model="crtModule.visible"
      :title="crtModule.title"
      :footer="null"
      width="1000px"
      class="dark-ant-modal"
    >
      <div class="clear h500 show-module">
        <module-content
          v-if="crtModule.visible"
          :module="modules1[crtModule.index]"
          :len="230"
          :dot-size="6"
          :preview="true"
        ></module-content>
      </div>
    </a-modal>-->

    <move-modal
      v-model="crtModule.visible"
      :title="crtModule.title"
      :isCenter="true"
      :fullscreen.sync="crtModule.fullscreen"
      @size-change="modalSizeChange"
    >
      <module-content
        v-if="crtModule.visible"
        :module="modules[crtModule.index]"
        :len="230"
        :dot-size="6"
        :preview="true"
        ref="mc"
      ></module-content>
    </move-modal>

    <finger-input ref="fingerInput" userId="3"></finger-input>
    <finger-check ref="fingerCheck" @inputCompleted="inputCompleted"></finger-check>
    <report id="111" ref="reportEle" :is-show.sync="isShowReport" @submit="reportSubmit"></report>
    <audio src="../../assets/warning.mp3" loop ref="baojing"></audio>
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
    import MoveModal from '../../components/MoveModal'
    import Report from '../../components/Report'
    import remoteHub from '../../utils/signalr'
    import storage from '../../utils/storage'
    import moment from 'moment'
    import { deviceEnquire } from '../../utils/device'

    var vm
    let baojingCount = 5

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
            Report,
            MoveModal
        },

        data() {
            return {
                crtModule: { visible: false, title: 'sfe', index: -1, fullscreen: false },
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
                reportText: '',
                butCount: 0, //需要抄表模块的数量
                tem: '',
                humidity: '',
                bjPolice: {
                    state: false,
                    index: 0,
                    id: 0,
                    min: 0,
                    max: 0,
                    value: 0
                }
            }
        },

        mounted() {
            vm = this
            const _self = this
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

                if (_self.bjPolice.state) {
                    this.$http
                        .post('/CallThe/CallThePolice/SaveData', {
                            DepartmentId: _self.tabActive,
                            DeviceInfoId: _self.bjPolice.id,
                            Max: _self.bjPolice.max,
                            Min: _self.bjPolice.min,
                            Value: _self.bjPolice.value
                        })
                        .then(res => {
                            console.log(res)
                        })
                        .catch(res => {
                            console.log(res)
                        })
                        .finally(res => {
                            console.log(res)
                        })
                }
            }, 1000)

            //数据推送
            remoteHub.acceptMessage('DeviceData', (datas, moduleId) => {
                if (_self.tabActive == moduleId) {
                    let newDatas = []
                    for (let index = 0; index < datas.length; index++) {
                        var model = {}
                        for (let name in datas[index]) {
                            model[name.substr(0, 1).toUpperCase() + name.substr(1)] = datas[index][name]
                        }
                        newDatas.push(model)
                    }

                    _self.datas = newDatas
                }

                _self.departMentList.forEach(item => {
                    if (item.Id == moduleId) {
                        item.onlineCount = _self.onlineCount
                    }
                })
            })

            //定时获取抄表
            remoteHub.acceptMessage('MeterReading', data => {
                _self.meterReadingBtn = data.disabled
                if (data.disabled) {
                    storage.removeLocal('btnDisabled')
                }

                _self.currentDate = new Date(data.datetime)
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

            this.$refs.baojing.load()
        },

        methods: {
            modalSizeChange() {
                this.$refs.mc.sizeChange()
            },
            //交接班
            handoverClick() {
                this.isShowReport = true
                this.$refs.reportEle.getData(this.tabActive)
            },

            reportSubmit(text) {
                this.$refs.fingerCheck.isShow = true
                this.reportText = text
            },

            inputCompleted(e) {
                if (e.type == 0) {
                    this.$http
                        .psot('/MeterReaDing/MeterReaDingOnDuty/GetUserIdByFingerTemplate', {
                            confirmuserid: e.data.template
                        })
                        .then(res => {
                            if (this.isShowReport) {
                                this.$http
                                    .post('/MeterReaDing/MeterReaDingOnDuty/PostReport', {
                                        id: res.UserId,
                                        deptId: this.tabActive,
                                        remarks: this.reportText
                                    })
                                    .finally(f => {
                                        this.$refs.fingerCheck.false = true
                                    })
                            } else {
                                this.$http
                                    .post('/MeterReaDing/MeterReaDingOnDuty/SaveData', {
                                        deptId: this.tabActive,
                                        confirmuserid: res.UserId
                                    })
                                    .then(res => {
                                        if (res.Success == true) {
                                            this.$message.success('抄表完成')
                                        } else {
                                            this.$message.error(res.message)
                                        }
                                    })
                                    .finally(res => {
                                        this.$refs.fingerCheck.false = true
                                    })
                            }
                        })
                } else if (e.type == 1) {
                    if (this.isShowReport) {
                        this.$http
                            .post('/MeterReaDing/MeterReaDingOnDuty/PostReport', {
                                id: e.data,
                                deptId: this.tabActive,
                                remarks: this.reportText
                            })
                            .finally(f => {
                                this.$refs.fingerCheck.false = true
                            })
                    } else {
                        this.$http
                            .post('/MeterReaDing/MeterReaDingOnDuty/SaveData', {
                                deptId: this.tabActive,
                                confirmuserid: e.data
                            })
                            .then(res => {
                                if (res.Success == true) {
                                    this.$message.success('抄表完成')
                                } else {
                                    this.$message.error(res.message)
                                }
                            })
                            .finally(res => {
                                this.$refs.fingerCheck.false = true
                            })
                    }
                }
            },

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
                    this.handleLogout()
                } else if (menu.key == 'hkv') {
                    window.open('http://192.168.1.12:1000/cn/demo.html', '_blank')
                }
            },

            tabChange(key) {
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

            getBattyArr(device) {
                var arr = []
                for (let i = 0; i < 24; i++) {
                    arr[i] = { index: i }
                }

                if (device) {
                    var props = device.propList || []
                    var prop = props.find(p => p.propName == 'voltage') //"voltage"
                    if (prop) {
                        var value = prop.value || '[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]'
                        if (value) {
                            let aa = JSON.parse(value)

                            if (aa instanceof Array && aa.length == 24) {
                                return aa.map((v, i) => {
                                    return { index: i, value: v }
                                })
                            }
                        }
                    }
                }

                return arr
            },

            butClick(index, model) {
                var vv = storage.getLocal('btnDisabled')
                if (!vv) vv = []
                vv.push({ btn: this.tabActive + '_' + model.moduleId, value: true })
                storage.setLocal('btnDisabled', vv)

                if (vv.length == this.butCount) {
                    this.$refs.fingerCheck.isShow = true
                }

                var mid
                var people = 1 // 1人工抄表 0自动抄表
                var timesetupid = '' //'1241755128407527424' //抄表时间 Id
                var confirmuserid = '' //'1242376184319184896' //指纹确认Id

                mid = model.moduleId

                var data = JSON.stringify(model)

                this.$http
                    .post('/MeterReaDing/MeterReaDingOnDuty/SaveData', {
                        jsondata: data,
                        mid: mid,
                        deptId: this.tabActive,
                        people: people,
                        timesetupid: timesetupid,
                        confirmuserid: confirmuserid
                    })
                    .then(res => {
                        if (res.Success == true) {
                            this.$message.success('抄表完成')
                        }
                    })
            },

            moduleClick(module, index) {
                debugger
                this.crtModule.visible = true
                this.crtModule.index = this.modules.indexOf(module)
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
                if (!item) return null

                var props = (item || {}).propList || []
                var prop = props.find(p => p.propName == 'voltage') //"voltage"
                if (!prop) return true

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

            getBtnDisabled(moduleId) {
                var vv = storage.getLocal('btnDisabled')
                if (vv) {
                    var b = vv.find(x => x.btn == this.tabActive + '_' + moduleId) || {}
                    return this.meterReadingBtn || b.value
                }
                return this.meterReadingBtn
            },

            baojing(device) {
                let _self = this

                this.$refs.baojing.play()
                this.bjPolice.id = device.deviceId
                this.bjPolice.max = device.value.max
                this.bjPolice.min = device.value.min
                this.bjPolice.value = device.value.value

                this.bjPolice.state = true

                if (_self.bjPolice.key) clearTimeout(this.bjPolice.key)

                this.bjPolice.key = setTimeout(() => {
                    _self.$refs.baojing.pause()
                    _self.bjPolice.state = false
                }, 1000)
            },

            sotp() {}
        },

        computed: {
            modules() {
                let _self = this
                var datas = []
                var moduleObj = _.groupBy(_self.dataStruct, 'DeviceDisplayModuleId')
                _.each(moduleObj, (module, moduleId) => {
                    var data1 = {
                        moduleId: moduleId,
                        moduleName: module[0].DeviceDisplayModuleName,
                        deviceTypeName: module[0].DeviceTypeName,
                        deviceTypeId: module[0].DeviceTypeId,
                        position: module[0].Position,
                        deviceList: [],
                        sort: module[0].Sort
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
                                if (target[key] != undefined) return target[key]

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
                            let obj = (_self.datas || []).find(x => x.DeviceId == deviceId && x.DevicePropId == propId)

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
                            if (data1.moduleId == '1241667491256602624') {
                                //debugger
                                console.log(_self)
                            }
                            data2['propList'].push(data3)
                        })
                    })
                })

                // this.allModuls.forEach(item => {
                //     if (!datas.find(x => x.moduleId == item.deviceDisplayModuleId)) {
                //         datas.push({
                //             moduleId: item.id,
                //             moduleName: item.deviceDisplayModuleName,
                //             position: item.position,
                //             sort: item.sort
                //         })
                //     }
                // })

                return datas
            },

            modules1() {
                var data = this.modules.filter(item => item.position == 1).sort((self, target) => self.sort - target.sort)
                this.butCount = data.length + 1
                return data
            },

            modules2() {
                var data = this.modules.filter(item => item.position == 2)
                if (data.length == 0) {
                    data.push({ moduleId: 0, deviceList: [] })
                }

                if (data[0].deviceList.length == 0) {
                    data[0].deviceList.push({})
                    data[0].deviceList.push({})
                } else if (data[0].deviceList.length == 1) {
                    data[0].deviceList.push({})
                }
                return data[0]
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
    #main-page .ant-divider-inner-text {
        color: white;
    }

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

    .my-card .ant-card-body {
        height: 300px;
    }
</style>
<style scoped>
    @media screen and (min-width: 576px) and (max-width: 768px) {
        .panel:nth-child(n + 1) {
            margin-top: 0px;
        }
    }

    @media screen and (min-width: 768px) and (max-width: 1200px) {
        .panel:nth-child(n + 3) {
            margin-top: 0px;
        }
    }

    @media screen and (min-width: 1200px) {
        .panel:nth-child(n + 5) {
            margin-top: 0px;
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
        margin: 3px;
        padding: 0 30px;
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
        width: 303px;
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

    * {
        color: white;
    }

    #main-page {
        background-color: #1b1b1b;
        overflow-x: hidden;
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
        top: 5px;
        right: 7px;
    }

    .main-title {
        margin: auto;
        text-align: center;
        font-size: 28px;
        color: #f2b428;
        font-weight: 800;
    }

    .panel-card {
        height: 330px;
    }

    .cell {
        /* width: 50px; */
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

    /* .line-box {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        margin-bottom: 8px;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    } */

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
        border: 1px solid white;
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
        top: 5;
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
        margin-top: 0 !important;
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

    .outer-box {
        margin-left: -12px;
        margin-right: -14px;
    }

    .table-btns {
        position: absolute;
        bottom: 0;
    }

    .table-btns td {
        vertical-align: bottom;
        height: 30px;
    }

    .table-btns .btn-big {
        text-align: right;
    }

    .table-btns .btn-big button {
        height: 54px;
    }

    .my-btn:not([disabled]) {
        background: #f2b428;
    }
</style>
