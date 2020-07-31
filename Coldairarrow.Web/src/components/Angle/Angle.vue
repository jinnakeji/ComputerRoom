<template>
  <div class="round" ref="round">
    <div class="box" :style="{ maxWidth: size + 'px', maxHeight: size + 'px'}">
      <div class="imgs">
        <div class="img axis" ref="roundImg"></div>
        <div class="img r"></div>
        <div class="value" :style="getAxis()"></div>
      </div>
      <div class="f f1 big">东</div>
      <div class="f f2 big">南</div>
      <div class="f f3 big">西</div>
      <div class="f f4 big">北</div>
      <div class="f f12" style="margin:30px">东南</div>
      <div class="f f32" style="margin:30px">西南</div>
      <div class="f f34" style="margin:30px">西北</div>
      <div class="f f14" style="margin:30px">东北</div>
    </div>
  </div>
</template>

<script>
    export default {
        props: {
            x: 0,
            y: 0,
            len: {
                type: Number,
                default: 55
            },
            dotSize: {
                type: Number,
                default: 3
            }
        },
        data() {
            return {
                size: 0
            }
        },
        mounted() {
            this.reload()
        },
        methods: {
            getSizeStyle() {
                return { width: this.size + 'px', height: this.size + 'px' }
            },
            getAxis() { 
                debugger
                let center = (this.size - 0) / 2

                var x = (this.x / 120) * center + center
                var y = (this.y / 120) * center + center
                const top = this.len
                const left = this.len
                if (x || y) {
                    let style = {
                        top: parseFloat(y) + 'px',
                        left: parseFloat(x) + 'px',
                        width: this.dotSize + 'px',
                        height: this.dotSize + 'px'
                    }
                    return style
                } else return {}
            },
            reload() {
                var rect = this.$refs.round.getBoundingClientRect()
                var min = rect.width > rect.height ? rect.height : rect.width
                this.size = min
            }
        }
    }
</script>

<style scoped>
    .round {
        position: relative;
        width: 100%;
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .imgs {
        position: absolute;
        width: 100%;
        height: 100%;
        padding: 20px;
    }

    .imgs .img {
        width: calc(100% - 40px);
        height: calc(100% - 40px);
    }

    .round .img {
        position: absolute;
        background-size: contain;
        background-color: transparent;
        border-radius: 2000px;
    }

    .round .img.axis {
        background-image: url(../../assets/axis.png);
    }

    .round .img.r {
        background-image: url(../../assets/round.gif);
        background-color: transparent;
    }

    .round .f {
        position: absolute;
    }

    .round .f.big {
        font-size: 15px;
    }

    .round .f1 {
        right: 0;
        top: calc(50% - 10px);
    }

    .round .f2 {
        bottom: 0;
        left: calc(50% - 10px);
    }

    .round .f3 {
        top: calc(50% - 10px);
    }

    .round .f4 {
        left: calc(50% - 10px);
    }

    .round .f12 {
        bottom: 0;
        right: 0;
    }

    .round .f32 {
        bottom: 0;
    }

    .round .f34 {
        right: 0;
    }

    .round .value {
        left: 0;
        top: 0;
        position: absolute;
        background: red;
        border-radius: 10px;
    }

    .box {
        width: 100%;
        height: 100%;
        position: relative;
    }
</style>