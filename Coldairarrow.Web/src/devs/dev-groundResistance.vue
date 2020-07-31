<template></template>
<script>
    import { police } from './range'
    export default {
        props: {
            deviceName: String,
            prop: {
                resistance: {
                    propType: String,
                    displayName: String,
                    max: Number,
                    min: Number,
                    value: String
                }
            }
        },
        data() {
            return {}
        }
    }
</script>