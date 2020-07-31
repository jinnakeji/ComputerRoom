<template></template>

<script>
    import { police } from './range'
    export default {
        props: {
            deviceName: String,
            prop: {
                temperature: {
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