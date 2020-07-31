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
                },
                temperatureOriginal: {
                    propType: String,
                    displayName: String,
                    max: Number,
                    min: Number,
                    value: String
                },
                xAngel: {
                    propType: String,
                    displayName: String,
                    max: Number,
                    min: Number,
                    value: String
                },
                xAngelOriginal: {
                    propType: String,
                    displayName: String,
                    max: Number,
                    min: Number,
                    value: String
                },
                yAngel: {
                    propType: String,
                    displayName: String,
                    max: Number,
                    min: Number,
                    value: String
                },
                yAngelOriginal: {
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
                }
            }
        },
        data() {
            return {}
        }
    }
</script>