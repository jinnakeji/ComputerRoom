<template>
  <div class="finger" v-if="isShow">
    <div class="bg"></div>
    <div class="box">
      <div class="finger-title">
        <label class="title" @click="changeState">{{ state == 0 ? "录入指纹" : "账号登录"}}</label>
        <a-icon class="close" @click="close" type="close" />
      </div>
      <template v-if="state == 0">
        <div class="finger-body">
          <div>
            <img :src="fingerImg" />
          </div>
        </div>
        <div class="finger-footer">
          <span class="msg">{{ msg }}</span>
        </div>
      </template>
      <template v-else>
        <div class="finger-body">
          <div>
            <div class="line">
              <label>账号:</label>
              <input type="text" v-model="userName" name="userName" />
            </div>
            <div class="line">
              <label>密码:</label>
              <input type="password" v-model="password" name="password" />
            </div>
            <div class="line">
              <button style="margin-left:30px" @click="submitClick">登录</button>
            </div>
          </div>
        </div>
      </template>
    </div>
  </div>
</template>

<script>
    import { Axios } from '../../utils/plugin/axios-plugin'
    import FClient from '../_util/fingerclient'
    import fingerImg from '../../assets/finger.gif'

    var fclient = new FClient()

    export default {
        props: {},
        data() {
            return {
                state: 0,
                isShow: false,
                msg: '',
                fingerImg: fingerImg,
                url: 'ws://localhost:8055',
                userName: '',
                password: ''
            }
        },
        methods: {
            changeState() {
                this.state = this.state == 0 ? 1 : 0
            },

            close() {
                this.isShow = false
            },

            message(e) {
                debugger
                if (e.data.type == 'finger') {
                    this.fingerImg = 'data:image/png;base64,' + e.data.image
                    this.$emit('inputCompleted', { type: 0, data: e.data })
                }
            },

            submitClick() {
                this.$http
                    .post('/Base_Manage/Home/CheckUser', { userName: this.userName, password: this.password })
                    .then(res => {
                        debugger
                        if (res.success) {
                            this.$emit('inputCompleted', { type: 1, data: res.data.userId })
                        } else {
                            alert(res.msg)
                        }
                    })
                    .catch(e => {
                        alert('出错了')
                    })
            }
        },
        watch: {
            isShow(newValue) {
                debugger
                if (newValue) {
                    this.msg = '请录入指纹'
                    fclient.connectionServer(this.url)
                    fclient.on('message', this.message.bind(this))
                    fclient.on('open', () => {
                        fclient.startMonitor()
                    })
                    fclient.on('close', () => {
                        console.log('ws: end server')
                    })
                } else {
                    fclient.endMonitor()
                }
            }
        }
    }
</script>

<style scoped>
    .title {
        cursor: pointer;
        -webkit-user-select: none;
    }

    .finger {
        position: fixed;
        width: 100%;
        height: 100%;
        left: 0;
        top: 0;
        z-index: 100000;
        display: flex;
    }

    .finger .bg {
        height: 100%;
        width: 100%;
        background: black;
        opacity: 0.5;
        position: absolute;
    }

    .finger .box {
        width: 230px;
        height: 300px;
        background-color: white;
        position: relative;
        margin: auto;
        display: flex;
        flex-direction: column;
    }

    .finger-title {
        height: 36px;
        display: inline-block;
        width: 100%;
        background: #001529;
    }

    .finger-title label {
        vertical-align: middle;
        line-height: 36px;
        margin-left: 10px;
        color: white;
    }

    .finger-body {
        flex-grow: 2;
        display: flex;
    }

    .finger-body > div {
        height: 100%;
        width: 100%;
        box-sizing: border-box;
        padding: 10px;
    }

    .finger-body img {
        width: 100%;
        height: 160px;
        margin-top: 32px;
    }

    .finger-body * {
        color: black !important;
    }

    .finger-footer {
        text-align: center;
    }

    .finger .msg {
        margin-left: 10px;
    }

    .close {
        color: white;
        float: right;
        top: 10px;
        right: 10px;
        position: absolute;
        cursor: pointer;
    }

    .close:hover {
        color: red;
    }

    .line {
        margin-bottom: 10px;
    }
</style>