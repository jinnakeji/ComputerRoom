<template></template>
<script>
    import { police } from './range'
    export default {
        props: {
            deviceName: String,
            prop: {
                current: {
                    propType: String,
                    displayName: String,
                    max: Number,
                    min: Number,
                    value: String
                },
                resistance: {
                    propType: String,
                    displayName: String,
                    max: Number,
                    min: Number,
                    value: String
                },
                temperature: {
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
                }
            }
        },
        data() {
            return {}
        }
    }
</script>