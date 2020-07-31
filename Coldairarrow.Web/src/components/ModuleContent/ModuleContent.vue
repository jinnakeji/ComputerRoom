<template>
  <div :class="{preview,'mc-content':true}">
    <!--三相电-->
    <div v-if="module.deviceTypeName == 'threeElec'">
      <template v-if="module.deviceList && module.deviceList.length > 0">
        <table class="text-center three-electric" v-if="preview === false">
          <thead>
            <tr>
              <th>设备名</th>
              <th>电压(V) | 电流(A)</th>
              <th>电压(V) | 电流(A)</th>
              <th>电压(V) | 电流(A)</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="device in module.deviceList" :key="device.deviceId">
              <td>{{ device.deviceName }}</td>
              <td>
                <span class="thel">{{ getArrayItem(device.voltage.value, 0) }}</span>
                <span class="thel">{{ getArrayItem(device.current.value, 0) }}</span>
              </td>
              <td>
                <span class="thel">{{ getArrayItem(device.voltage.value, 1) }}</span>
                <span class="thel">{{ getArrayItem(device.current.value, 1) }}</span>
              </td>
              <td>
                <span class="thel">{{ getArrayItem(device.voltage.value, 2) }}</span>
                <span class="thel">{{ getArrayItem(device.current.value, 2) }}</span>
              </td>
            </tr>
          </tbody>
        </table>
        <div v-else>
          <div
            v-for="device in module.deviceList"
            :key="device.deviceId"
            style="margin-bottom:20px"
          >
            <table class="border-table text-center" style="width:100%">
              <thead>
                <tr>
                  <th>{{ device.deviceName }}</th>
                  <th>电压（V）</th>
                  <th>电流（A）</th>
                  <th>有功功率（W）</th>
                  <th>无功功率（vra）</th>
                  <th>视在功率（vra）</th>
                  <th>功率因数（cos）</th>
                  <th>频率（Hz）</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>A项</td>
                  <td>{{ getArrayItem(device.voltage.value, 0) }}</td>
                  <td>{{ getArrayItem(device.current.value, 0) }}</td>
                  <td>{{ getArrayItem(device.power.value, 0) }}</td>
                  <td>{{ getArrayItem(device.qpower.value, 0) }}</td>
                  <td>{{ getArrayItem(device.spower.value, 0) }}</td>
                  <td>{{ getArrayItem(device.powerNumber.value, 0) }}</td>
                  <td>{{ getArrayItem(device.freq.value, 0) }}</td>
                </tr>
                <tr>
                  <td>B项</td>
                  <td>{{ getArrayItem(device.voltage.value, 1) }}</td>
                  <td>{{ getArrayItem(device.current.value, 1) }}</td>
                  <td>{{ getArrayItem(device.power.value, 1) }}</td>
                  <td>{{ getArrayItem(device.qpower.value, 1) }}</td>
                  <td>{{ getArrayItem(device.spower.value, 1) }}</td>
                  <td>{{ getArrayItem(device.powerNumber.value, 1) }}</td>
                  <td>{{ getArrayItem(device.freq.value, 1) }}</td>
                </tr>
                <tr>
                  <td>C项</td>
                  <td>{{ getArrayItem(device.voltage.value, 2) }}</td>
                  <td>{{ getArrayItem(device.current.value, 2) }}</td>
                  <td>{{ getArrayItem(device.power.value, 2) }}</td>
                  <td>{{ getArrayItem(device.qpower.value, 2) }}</td>
                  <td>{{ getArrayItem(device.spower.value, 2) }}</td>
                  <td>{{ getArrayItem(device.powerNumber.value, 2) }}</td>
                  <td>{{ getArrayItem(device.freq.value, 2) }}</td>
                </tr>
              </tbody>
            </table>
            <div>
              <span style="float:left">节点号：1</span>
              <span style="float:right">更新时间：{{ device.firstProp.date }}</span>
            </div>
          </div>
        </div>
      </template>
      <template v-else>
        <div class="nothing">暂无数据</div>
      </template>
    </div>
    <!--铁塔角度-->
    <div v-else-if="module.deviceTypeName == 'angel'" style="height:100%">
      <template v-if="module.deviceList && module.deviceList.length > 0">
        <div
          class="line"
          style="width:100%;height:100%"
          v-for="device in module.deviceList"
          :key="device.deviceId"
        >
          <div class="left angle">
            <div class="x-y-c">
              <div class="field">
                <span class="name">X轴</span>
                <span class="value" style="width:80px">{{ device.xAngel.value }}</span>
                <span class="unit">{{ device.xAngel.propUnit }}</span>
              </div>
              <div class="field">
                <span class="name">Y轴</span>
                <span class="value" style="width:80px">{{ device.yAngel.value }}</span>
                <span class="unit">{{ device.yAngel.propUnit }}</span>
              </div>
              <div class="field">
                <span class="name">温度</span>
                <span class="value" style="width:80px">{{ device.temperature.value }}</span>
                <span class="unit">{{ device.temperature.propUnit }}</span>
              </div>
            </div>
          </div>
          <div class="right">
            <angle ref="angle" :x="device.xAngel.value" :y="device.yAngle.value"></angle>
          </div>
        </div>
      </template>
      <template v-else>
        <div class="nothing">暂无数据</div>
      </template>
    </div>
    <div v-else-if="module.deviceTypeName == 'battery'">
      <table class="border-table" style="height:90px">
        <tr>
          <td>蓄电池</td>
          <td>总电压</td>
          <td class="cell" v-for="index in 24" :key="index">{{ index }}#</td>
        </tr>
        <tr>
          <td>+48</td>
          <td>+{{ (module.deviceList[0].voltageTotal || {}).value }}</td>
          <td
            v-for="data in getBattyArr(module.deviceList[0])"
            :class="{ red: policeBattery(module.deviceList[0], data.value) }"
            :key="data.index"
          >{{ data.value }}</td>
        </tr>
        <tr>
          <td>-48</td>
          <td>-{{ (module.deviceList[1].voltageTotal || {}).value }}</td>
          <td
            v-for="data in getBattyArr(module.deviceList[1])"
            :class="{ red: policeBattery(module.deviceList[1], data.value) }"
            :key="data.index"
          >-{{ data.value }}</td>
        </tr>
      </table>
    </div>
    <div v-else class="line-even">
      <template v-if="module.deviceList && module.deviceList.length > 0">
        <template v-if="preview">
          <table class="border-table">
            <tr v-for="device in module.deviceList" :key="device.deviceId">
              <td>{{ device.deviceName }}</td>
              <td>
                <span v-for="prop in device.propList" :key="prop.propId" class="block">
                  <template
                    v-if="device.deviceTypeName == 'a5NodeOnOff'"
                  >[{{ prop.displayName || prop.propName }} : {{ prop.value == 0 ? "关" : prop.vlaue == -1 ? "开" : "无" }}]</template>
                  <template
                    v-else-if="device.deviceTypeName == 'aaNodeOnOff'"
                  >[{{ prop.displayName || prop.propName }} : {{ prop.value == 0 ? "关" : prop.vlaue == -1 ? "开" : "无" }}]</template>
                  <template
                    v-else-if="device.deviceTypeName == 'airOnOff'"
                  >[{{ prop.displayName || prop.propName }} : {{ prop.value == 240 ? "合闸" : prop.vlaue == 15 ? "分闸" : "无" }}]</template>
                  <template
                    v-else
                  >[{{ prop.displayName || prop.propName }} : {{ prop.value || '无' }}]</template>
                </span>
              </td>
            </tr>
          </table>
        </template>
        <template v-else>
          <div class="line" v-for="device in module.deviceList" :key="device.deviceId">
            <label class="device field">
              <span class="name">{{ device.deviceName }}</span>
              <div class="values">
                <label v-for="prop in getShowPropList(device.propList)" :key="prop.propId">
                  <template v-if="device.deviceTypeName == 'a5NodeOnOff'">
                    <span :class="{dot:true, red:prop.value == 0 }"></span>
                  </template>
                  <template v-else-if="device.deviceTypeName == 'aaNodeOnOff'">
                    <span
                      :class="{switch : true, close:prop.value == -1}"
                    >{{ prop.value == 0 ? "关" : "开" }}</span>
                  </template>
                  <template v-else-if="device.deviceTypeName == 'airOnOff'">
                    <span
                      class="switch"
                      :title="prop.displayName || prop.propName"
                    >{{ prop.value == 240 ? "合闸" : prop.vlaue == 15 ? "分闸" : "无" }}</span>
                  </template>
                  <template v-else>
                    <span
                      class="switch"
                      :title="prop.displayName || prop.propName"
                      :class="{ value: true, red: police(prop,device) }"
                    >{{ prop.value }}</span>
                    <span class="unit">{{ prop.propUnit}}</span>
                  </template>
                </label>
              </div>
            </label>
          </div>
        </template>
      </template>
      <template v-else>
        <div class="nothing">暂无数据</div>
      </template>
    </div>
  </div>
</template>

<script>
    import Angle from '../Angle'

    export default {
        components: {
            Angle
        },
        props: {
            module: {
                type: Object,
                default() {
                    return null
                }
            },
            len: {
                type: Number,
                default: 55
            },
            dotSize: {
                type: Number,
                default: 3
            },
            preview: {
                type: Boolean,
                default: false
            }
        },
        methods: {
            police(prop, device) {
                if (prop.value) {
                    var ret = false
                    var min = prop.min
                    var max = prop.max
                    var value = prop.value
                    if (prop.propType === 'int' || prop.propType === 'double') {
                        min = parseFloat(prop.min)
                        max = parseFloat(prop.max)
                        value = parseFloat(prop.value)
                    }
                    if (min) {
                        if (value < min) ret = true
                    }
                    if (max) {
                        if (value > max) ret = true
                    }

                    if (ret) {
                        this.$emit('wanring', device)
                    } else {
                        this.$emit('stop')
                    }

                    return ret
                }
            },

            getArrayItem(array, index) {
                if (!array) return ''
                var arr = JSON.parse(array)
                if (arr instanceof Array) {
                    if (arr.length >= index) {
                        return arr[index]
                    }
                    return ''
                }
            },

            getAxis(device) {
                const x = device.xAngel
                const y = device.yAngel
                const top = this.len
                const left = this.len
                if (x.value || y.value) {
                    let style = {
                        top: top + -parseFloat(y.value) + 'px',
                        left: left + parseFloat(x.value) + 'px',
                        width: this.dotSize + 'px',
                        height: this.dotSize + 'px'
                    }
                    return style
                } else return {}
            },

            getShowPropList(propList) {
                if (this.preview) {
                    return propList
                }
                return propList.filter(x => x.propIsShow)
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

            sizeChange() {
                var _self = this
                setTimeout(() => {
                    this.$refs.angle[0].reload()
                })
            }
        }
    }
</script>

<style scoped>
    .mc-content {
        height: 100%;
        overflow-x: hidden;
        overflow-y: auto;
    }

    .three-electric {
        /* height: 100%; */
        width: 100%;
    }

    .three-electric th {
        padding: 7px 0px;
    }

    .three-electric th,
    .three-electric td {
        font-size: 15px;
    }

    .three-electric span {
        width: 60px;

        display: inline-block;
        height: 32px;
        padding: 2px;
        vertical-align: middle;
        font-size: 20px;
        font-weight: 900;
        border: 1px solid darkslategray;
        color: green;
    }

    .three-electric span + span {
        border-left: 1px solid gray;
    }

    table.text-center th,
    table.text-center td {
        text-align: center;
    }

    .field .name {
        margin-right: 4px;
        width: 100px;
        display: inline-block;
        vertical-align: top;
        text-overflow: ellipsis;
        white-space: nowrap;
        overflow: hidden;
        font-size: 16px;
    }

    .field .value {
        margin-left: 4px;
        color: green;
        width: 70px;
        height: 30px;
        vertical-align: top;
        display: inline-block;
        text-align: center;

        border-radius: 2px;
        font-size: 20px;
        font-weight: 900;
        border: 1px solid darkslategray;
    }

    .field .value.red {
        color: red;
    }

    .left {
        float: left;
        width: 35%;
        height: 100%;
    }

    .right {
        float: right;
        width: 65%;
        height: 100%;
    }

    .border-table td,
    .border-table th {
        border: 1px solid #000000;
        color: #000000;
    }

    .text-center td,
    .text-center th {
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

    .line-even::after {
        content: '';
        display: block;
        position: relative;
        clear: both;
    }

    /* .line-even .line:nth-child(even)::before {
                                        content: '';
                                        display: block;
                                        position: absolute;
                                        width: 1px;
                                        height: 17px;
                                        border-left: 1px solid gray;
                                        margin-left: -6px;
                                        margin-top: 1px;
                                    }

                                    .show-module .line-even .line:nth-child(even)::before {
                                        content: '';
                                        display: block;
                                        position: absolute;
                                        width: 1px;
                                        height: 30px;
                                        border-left: 1px solid gray;
                                        margin-left: -6px;
                                        margin-top: 1px;
                                    } */

    .line {
        border-radius: 3px;
        padding: 4px;
        position: relative;
        /* width: 50%; */
        float: left;
        height: 40px;
    }

    .line > label {
        display: block;
        font-size: 13px;
    }

    .line .field {
        display: inline-block;
        font-size: 12px;
        border-radius: 2px;

        display: block;
    }

    .nothing {
        height: 187px;
        display: inline-block;
        text-align: center;
        width: 100%;
        vertical-align: middle;
        line-height: 187px;
    }

    .values {
        display: inline-block;
    }

    .values > label {
        display: inline-block;
    }

    .unit {
        margin-left: 4px;
        font-size: 16px;
        vertical-align: top;
    }

    .x-y-c {
        position: absolute;
        bottom: 20px;
    }

    .x-y-c .field {
        margin-bottom: 5px;
    }

    .x-y-c .name {
        width: 60px;
        display: inline-block;
        text-align-last: justify;
        margin-top: -3px;
    }

    .dot {
        display: inline-block;
        width: 16px;
        height: 16px;
        background: green;
        border-radius: 8px;
        margin-left: 4px;
        vertical-align: sub;
    }

    .dot.red {
        background: red;
    }

    .switch {
        display: inline-block;

        width: 70px;
        padding: 0 3px;
        color: green;
        text-align: right;
        border-radius: 4px;
        font-size: 20px;
        border: 1px solid darkslategray;
        font-weight: 900;
        margin-right: 9px;
        margin-left: 4px;
    }

    .switch.close {
        color: red;
        text-align: left;
    }

    table {
        width: 100%;
        font-size: 20px;
    }

    .preview th,
    .preview td {
        padding: 4px;
    }

    .preview * {
        color: black;
    }

    .block + .block {
        margin-left: 10px;
    }
</style>
