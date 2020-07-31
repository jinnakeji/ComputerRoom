<template>
  <div>
    <finger-check ref="fingerCheck" @inputCompleted="inputCompleted"></finger-check>
  </div>
</template>

<script>
    import FingerCheck from '../../components/FingerCheck'

    export default {
        components: {
            FingerCheck
        },

        mounted() {
            this.$refs.fingerCheck.isShow = true
        },

        methods: {
            inputCompleted(e) {
                debugger
            }
        }
    }
</script>