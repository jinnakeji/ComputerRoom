<template>
  <table class="border-table text-center three-electric">
    <thead>
      <tr>
        <th rowspan="2">设备名</th>
        <th colspan="2">A项</th>
        <th colspan="2">B项</th>
        <th colspan="2">C项</th>
      </tr>
      <tr>
        <th>电压(V)</th>
        <th>电流(A)</th>
        <th>电压(V)</th>
        <th>电流(A)</th>
        <th>电压(V)</th>
        <th>电流(A)</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="device in deviceList" :key="device.deviceId">
        <td>{{ device.deviceName }}</td>
        <td>{{ getArrayItem(device.prop.value,0) }}</td>
        <td>{{ getArrayItem(device.prop.value,0) }}</td>
        <td>{{ getArrayItem(device.prop.value,1) }}</td>
        <td>{{ getArrayItem(device.prop.value,1) }}</td>
        <td>{{ getArrayItem(device.prop.value,2) }}</td>
        <td>{{ getArrayItem(device.prop.value,2) }}</td>
      </tr>
    </tbody>
  </table>
</template>
<script>
    import { police } from './range'
    export default {
        props: {
            deviceList: [
                {
                    deviceName: String,
                    prop: {
                        totalPower: {
                            propType: String,
                            displayName: String,
                            max: Number,
                            min: Number,
                            value: String
                        },
                        current: {
                            propType: String,
                            displayName: String,
                            max: Number,
                            min: Number,
                            value: String
                        },
                        voltage: {
                            propType: String,
                            displayName: String,
                            max: Number,
                            min: Number,
                            value: String
                        },
                        zeroCurrent: {
                            propType: String,
                            displayName: String,
                            max: Number,
                            min: Number,
                            value: String
                        }
                    }
                }
            ]
        },
        methods: {
            getArrayItem(array, index) {
                if (!array) return ''
                var arr = JSON.parse(array)
                if (arr instanceof Array) {
                    if (arr.length >= index) {
                        return arr[index]
                    }
                    return ''
                }
            }
        }
    }
</script>