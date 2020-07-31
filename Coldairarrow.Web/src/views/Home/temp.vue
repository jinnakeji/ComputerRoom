/* eslint-disable vue/no-template-shadow */
<template>
  <div>
    <a-tabs v-model="tabActive" @change="tabChange">
      <div slot="tabBarExtraContent" class="panel-title">
        <a href="/Home/Introduce" style="color: white;" target="_blank">智慧机房值班管控系统</a>
      </div>
      <a-tab-pane
        v-for="item in departMentList"
        :key="item.Id"
        :tab="item.Name"
        @mouseover="tabMouseOver"
        @mouseout="tabMouseOut"
      >
        <div class="line-box" v-if="modules1.length > 0">
          <a-row :gutter="24">
            <a-col :sm="24" :md="12" :xl="6" v-for="mod in modules1" :key="mod.moduleId">
              <a-card :title="mod.moduleName" height="100px">
                <template v-if="mod.deviceList && mod.deviceList.length > 0">
                  <div class="line" v-for="device in mod.deviceList" :key="device.deviceId">
                    <label>{{ device.deviceName }}</label>
                    <div class="field" v-for="prop in device.propList" :key="prop.propId">
                      <span class="name">{{ prop.displayName || prop.propName }}:</span>
                      <span :class="{ value: true, red: police(prop) }">{{ prop.value }}</span>
                    </div>
                  </div>
                  <div>
                    <a-button
                      class="big"
                      size="small"
                      type="primary"
                      slot="extra"
                      @click="butCick(1, mod)"
                    >值班抄表</a-button>
                  </div>
                </template>
                <template v-else>
                  <div>暂无数据</div>
                </template>
              </a-card>
            </a-col>
          </a-row>
        </div>

        <div class="line-box">
          <table class="table-box" v-for="item in modules2" :key="item.moduleId">
            <tr>
              <td style="width: 100px;">
                <img class="img" :src="imgUrl" />
              </td>
              <td style="width: 45px;">
                <button class="ant-btn ant-btn-primary ant-btn-sm btn-vertical">
                  <span>值班抄表</span>
                </button>
              </td>
              <td style="width: 80px;background-color: #1890ff;" class="tbox">
                <a-button>充电</a-button>
                <a-button>放电</a-button>
              </td>
              <td>
                <table class="battery-table">
                  <tr>
                    <td>总电压</td>
                    <td v-for="index in 24" :key="index">{{ index }}</td>
                  </tr>
                  <tr>
                    <td>+</td>
                    <td
                      v-for="data in getBattyArr(item)"
                      :class="{ red: policeBattery(item, data.value) }"
                      :key="data.index"
                    >{{ data.value }}</td>
                  </tr>
                  <tr>
                    <td>总电压</td>
                    <td v-for="index in 24" :key="index">{{ index }}</td>
                  </tr>
                  <tr>
                    <td>-</td>
                    <td
                      v-for="data in getBattyArr(item)"
                      :class="{ red: policeBattery(item, data.value) }"
                      :key="data.index"
                    >-{{ data.value }}</td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>

        <div class="line-box" style="height: 150px;">
          <table class="table-box" style="height:100%">
            <tr>
              <td style="width: 100px;">
                <img class="img" :src="imgswitch" />
              </td>
              <td>
                <table class="battery-table" style="height: 100%;">
                  <tr>
                    <td>开关名称</td>
                    <td v-for="item in switchList" :key="item.index">{{ item.name }}</td>
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
                      <img
                        v-if="item.has"
                        class="switch-img"
                        :src="item.value == 1 ? imgon : imgoff"
                      />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
      </a-tab-pane>
    </a-tabs>

    <!-- <div class="line-box" style="height: 240px;width: 100%; overflow-x: scroll;">
      <div class="video">
        <iframe
          class="f1"
          src="http://localhost:1000/cn/index.html"
          style="border:0 ; overflow-y:none;sco;overflow-x : hidden;"
          scrolling="no"
          width="1000px"
          height="200px"
          boder="0"
        ></iframe>
        <iframe
          class="f2"
          src="http://localhost:1000/cn/index1.html"
          style="border:0 ; overflow-y:none;sco;overflow-x : hidden;"
          scrolling="no"
          width="1000px"
          height="200px"
          boder="0"
        ></iframe>
        <iframe
          class="f3"
          src="http://localhost:1000/cn/index2.html"
          style="border:0 ; overflow-y:none;sco;overflow-x : hidden;"
          scrolling="no"
          width="1000px"
          height="200px"
          boder="0"
        ></iframe>
      </div>
    </div>-->
  </div>
</template>

<script>
    import ChartCard from '@/components/ChartCard/ChartCard'
    import Trend from '@/components/ChartCard/Trend'
    import imgUrl from '../../assets/battery.png'
    import imgswitch from '../../assets/switch.png'
    import imgon from '../../assets/on.png'
    import imgoff from '../../assets/off.png'
    import _ from 'underscore'

    // setInterval(() => {
    //   // eslint-disable-next-line eqeqeq
    //   var index = vm.data.findIndex(item => item.Id == vm.tabActive)
    //   if (index >= vm.data.length - 1) {
    //     index = -1
    //   }
    //   console.log(index)
    //   vm.tabActive = vm.data[index + 1].Id;
    //   vm.tabChange();
    // }, 5000)

    var vm

    export default {
        components: {
            ChartCard,
            Trend
        },
        data() {
            return {
                tabActive: '',
                datas: [],
                allModuls: [],
                data: [],
                imgUrl: imgUrl,
                imgswitch: imgswitch,
                imgon: imgon,
                imgoff: imgoff,
                DeviceModuledata: [],
                departMentList: [],
                dataStruct: []
            }
        },
        // 在这里调用ajax请求方法
        mounted() {
            vm = this
            var p1 = this.$http.get('/Home/Index/DepartmentList')
            var p2 = this.$http.get('/Home/Index/ModulesList')

            Promise.all([p1, p2]).then(res => {
                let res1 = res[0]
                let res2 = res[1]

                if (res1.Data.length > 0) this.tabActive = res1.Data[0].Id
                else this.tabActive = ''
                this.tabChange()
                this.departMentList = res1.Data

                this.allModuls = res2.Data
            })
        },
        methods: {
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
            },

            tabMouseOver() {
                console.log('over')
            },

            tabMouseOut() {
                console.log('out')
            },

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
                console.log(arr)

                return arr
            },

            butCick(index, model) {
                //debugger
                var mid
                if (index == 1) {
                    mid = model.moduleId
                }

                var data = JSON.stringify(model)
                //console.log(data)
                this.$http.post('/MeterReaDing/MeterReaDingOnDuty/SaveData', { jsondata: data, mid: mid }).then(res => {
                    if (res.Success == true) {
                        alert('抄表完成')
                    }
                })
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

            //报警值
            police(prop) {
                if (prop.value) {
                    var min = prop.min
                    var max = prop.max
                    var value = prop.value
                    if (prop.propType === 'int' || prop.propType === 'double') {
                        min = parseFloat(prop.min)
                        max = parseFloat(prop.max)
                        value = parseFloat(prop.value)
                    }
                    if (min) {
                        if (value < min) return true
                    }
                    if (max) {
                        if (value > max) return true
                    }
                }
            },
            policeBattery(item, value) {
                //console.log("电池："+item);

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
            }
        },

        computed: {
            modules() {
                var datas = []
                var moduleObj = _.groupBy(this.dataStruct, 'DeviceDisplayModuleId')
                _.each(moduleObj, (module, moduleId) => {
                    var data1 = {
                        moduleId: moduleId,
                        moduleName: module[0].DeviceDisplayModuleName,
                        position: module[0].Position,
                        deviceList: []
                    }
                    datas.push(data1)
                    var deviceObj = _.groupBy(module, 'Deviceid')
                    _.each(deviceObj, (device, deviceId) => {
                        let data2 = {
                            deviceId: deviceId,
                            deviceName: device[0].DeviceName,
                            propList: []
                        }
                        data1['deviceList'].push(data2)
                        var propObj = _.groupBy(device, 'DevicePropId')
                        _.each(propObj, (prop, propId) => {
                            let obj = this.datas.find(x => x.DeviceId == deviceId && x.DevicePropId == propId)
                            let data3 = {
                                propId: propId,
                                propName: prop[0].DevicePropName,
                                displayName: prop[0].DevicePropDisplayName,
                                max: prop[0].Max,
                                min: prop[0].Min,
                                propType: prop[0].PropType,
                                value: (obj || {}).Value
                            }
                            data2['propList'].push(data3)
                        })
                    })
                })

                this.allModuls.forEach(item => {
                    if (!datas.find(x => x.moduleId == item.Id)) {
                        datas.push({
                            moduleId: item.Id,
                            moduleName: item.DeviceDisplayModuleName,
                            position: item.IsDisplay
                        })
                    }
                })
                //console.log(datas);
                return datas
            },

            modules1() {
                var data = this.modules.filter(item => item.position == 1)
                //console.log(data)
                return data
            },

            modules2() {
                var data = this.modules.filter(item => item.position == 2)
                console.log(data)
                return data
            },

            modules3() {
                var data = this.modules.filter(item => item.position == 3)
                return data
            },

            switchList() {
                //console.log(this.modules3)
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
            }
        }
    }
</script>

<style>
    html,
    body {
        overflow-x: hidden !important;
    }

    .video {
        width: 2900px;
        height: 200px;
        position: relative;
    }
    .video iframe {
        width: 1000px;
        position: absolute;
    }

    .f2 {
        left: 1000px;
    }

    .f3 {
        left: 1957px;
    }

    .ant-tabs-extra-content {
        line-height: 40px;
        float: left !important;
    }

    .panel-title {
        margin: 0 20px;
        font-size: 20px;
    }

    .ant-tabs-bar.ant-tabs-top-bar {
        background-color: #393d49;
        color: white;
    }

    .table-box,
    .table-box > tr > td {
        height: 100px;
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

    .img {
        height: 100px;
        width: 100px;
    }

    .iframe {
        border: 0;
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
        background: white;
        padding: 10px;
        margin-bottom: 20px;
    }

    .line {
        background-color: #f0f0f0;
        border-radius: 3px;
        padding: 4px;
    }

    .line > label {
        display: block;
        font-size: 16px;
        font-weight: 700;
    }

    .btn-vertical {
        width: 45px;
        height: 100%;
    }

    .btn-vertical span {
        writing-mode: tb-rl !important;
    }

    .line .field {
        display: inline-block;
        background-color: #d6d6d6;
        border-radius: 2px;
        padding: 0 4px;
    }

    .line .field:first-child {
        margin-left: 20px;
    }

    .field + .field {
        margin-left: 10px;
    }

    .line + .line {
        margin-top: 6px;
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

    .battery-table {
        width: 100%;
    }

    .battery-table td {
        width: 4%;
        border: 1px solid gray;
        text-align: center;
    }

    .setTable {
        height: 128px;
        width: 80%;
    }

    .setTable > tr {
        height: 32px;
    }

    .setTable > thead > th {
        width: 20px;
        border: 1px solid;
        text-align: center;
    }

    .setTable > tr > td {
        width: 20px;
        border: 1px solid;
        text-align: center;
    }

    .batteryTitle {
        float: left;
        width: 8% !important;
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
</style>
