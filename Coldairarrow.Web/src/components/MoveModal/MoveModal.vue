<template>
  <div ref="doc" class="doc" v-if="value" @mousemove="omMousemove" @mouseup="onMouseUp">
    <div class="bg"></div>
    <div
      :class="{main: true, fullscreen: fullscreen}"
      ref="div"
      :style="{left: (fullscreen ? 0 : rect.left) + 'px', top: (fullscreen ? 0 : rect.top) + 'px'}"
    >
      <div class="title" @mousedown="onMousedown">
        <span>{{ title }}</span>
        <div class="tools-btn">
             <div class="btn close" @click="fullscreenClick">
            <a-icon type="minus-circle" />
          </div>
          <div class="btn" @click="fullscreenClick">
            <a-icon v-if="fullscreen" type="fullscreen-exit" />
            <a-icon v-else type="fullscreen" />
          </div>
          <div class="btn close" @click="close">
            <a-icon type="close-circle" />
          </div>
        </div>
      </div>
      <div class="body">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script>
    const width = 1700
    const height = 800

    export default {
        props: {
            value: false,
            title: '',
            isCenter: true,
            fullscreen: false
        },
        data() {
            return {
                rect: {
                    left: 0,
                    top: 0
                },
                init: {
                    left: 0,
                    top: 0
                },
                end: {
                    left: 0,
                    top: 0
                },
                isMove: false,
                height: height,
                width: width
            }
        },
        mounted() {
            document.body.appendChild(this.$el)
            var rect = document.body.getBoundingClientRect()
            var dd = { width: this.width, height: this.height }
            if (this.isCenter) {
                this.end.left = rect.width / 2 - dd.width / 2
                this.end.top = rect.height / 2 - dd.height / 2
                this.rect.left = this.end.left
                this.rect.top = this.end.top
            }
        },
        methods: {
            onMousedown(event) {
                this.isMove = true
                const { pageX, pageY } = event

                this.init.top = pageY
                this.init.left = pageX
            },

            omMousemove(event) {
                if (this.isMove == false) return

                const { pageX, pageY } = event

                let left = pageX - this.init.left + this.end.left
                let top = pageY - this.init.top + this.end.top

                if (left < 0) left = 0
                if (top < 0) top = 0

                this.rect.left = left
                this.rect.top = top
            },

            onMouseUp(event) {
                this.isMove = false
                this.end.left = this.rect.left
                this.end.top = this.rect.top
            },

            close() {
                this.$emit('input', false)
            },

            fullscreenClick() {
                this.$emit('update:fullscreen', !this.fullscreen)
                this.$emit('size-change')
            }
        }
    }
</script>

<style lang="less" scoped>
    @width: 1700px;
    @height: 800px;

    .doc {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
    }

    .bg {
        position: absolute;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        background-color: black;
        opacity: 0.5;
    }

    .main {
        background-color: #fafafa;
        position: absolute;
        width: @width;
        height: @height;

        &.fullscreen {
            width: 100%;
            height: 100%;
        }
    }

    .body {
        padding: 40px;
        height: calc(100% - 28px);
    }

    .title {
       background-color: #E4E4E4;
        cursor: move;
        color: black;
        font-size: 20px;
        font-weight: 900;

        span {
            display: inline-block;
            margin: 4px;
        }
    }

    .tools-btn {
        float: right;

        .btn {
            display: inline-block;
            font-size: 19px;
            cursor: pointer;
            width: 28px;
            text-align: center;

            &.close:hover {
                background-color: rgb(117, 114, 114);
            }
        }
    }
</style>