<template>
  <div class="line">
    <label>{{ deviceName }}</label>
    <div class="field">
      <span class="name">{{ prop.value.displayName || "onOff" }}</span>
      <span :class="{ value: true, red: police(prop.value) }">{{ prop.value.value }}</span>
    </div>
  </div>
</template>
<script>
    import { police } from './range'

    export default {
        props: {
            deviceName: String,
            prop: {
                value: {
                    displayName: String,
                    value: String,
                    max: Number,
                    min: Number
                }
            }
        },
        methods: {
            police: police
        }
    }
</script>