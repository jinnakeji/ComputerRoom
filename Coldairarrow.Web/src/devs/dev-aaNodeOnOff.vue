<template>
  <div class="line">
    <label>{{ deviceName }}</label>
    <div class="field">
      <span class="name">{{ prop.onOff.displayName || "onOff" }}</span>
      <span :class="{ value: true, red: police(prop.onOff) }">{{ prop.onOff.value }}</span>
    </div>
  </div>
</template>
<script>
    import { police } from './range'

    export default {
        props: {
            deviceName: String,
            prop: {
                onOff: {
                    propType: String,
                    displayName: String,
                    max: Number,
                    min: Number,
                    value: String
                }
            }
        },
        methods: {
            police: police
        }
    }
</script>