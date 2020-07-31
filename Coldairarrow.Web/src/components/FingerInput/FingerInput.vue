<template>
  <div class="finger" v-if="isShow">
    <div class="bg"></div>
    <div class="box">
      <div class="finger-title">
        <label>录入指纹</label>
        <a-icon class="close" @click="close" type="close" />
      </div>
      <div class="finger-body">
        <div v-for="item in fingerList" :key="item.id">
          <img :src="item.url" />
        </div>
      </div>
      <div class="finger-footer">
        <span class="msg">{{ msg }}</span>
        <button
          class="finger-btn ant-btn ant-btn-primary"
          @click="btnInput"
          :disabled="state == 'input'"
        >{{ btnText }}</button>
      </div>
    </div>
  </div>
</template>

<script>
import { Axios } from '../../utils/plugin/axios-plugin'
import FClient from '../_util/fingerclient'

var fclient = new FClient()

export default {
  props: {
    userId: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      isShow: false,
      count: 3,
      url: 'ws://localhost:8055',
      state: 'start',
      temp: '共需要录入三次指纹，还有{0}次。',
      fingerList: [
        { id: 1, url: require('../../assets/finger.jpg'), template: '' },
        { id: 2, url: require('../../assets/finger.jpg'), template: '' },
        { id: 3, url: require('../../assets/finger.jpg'), template: '' }
      ]
    }
  },

  methods: {
    init() {
      this.fingerList = [
        { id: 1, url: require('../../assets/finger.jpg'), template: '' },
        { id: 2, url: require('../../assets/finger.jpg'), template: '' },
        { id: 3, url: require('../../assets/finger.jpg'), template: '' }
      ]
      this.count = 3
      this.state = 'start'
    },

    btnInput() {
      if (this.state == 'start') {
        this.state = 'input'
        fclient.connectionServer(this.url)
        fclient.on('message', this.message.bind(this))
        fclient.on('open', () => {
          fclient.startMonitor()
        })
        fclient.on('close', () => {
          console.log('ws: end server')
        })
      } else if (this.state == 'input') {
      } else if (this.state == 'end') {
        fclient.endMonitor()
        this.$http
          .post('/Api/Finger/Input', {
            id: this.userId,
            temp1: this.fingerList[0].template,
            temp2: this.fingerList[1].template,
            temp3: this.fingerList[2].template
          })
          .then(res => {
            if (res.Success) {
              this.$emit('inputCompleted', {
                type: 'success'
              })
            } else {
              this.$emit('inputCompleted', {
                type: 'error',
                message: res.Message
              })
            }
          })
      }
    },

    message(e) {
      if (e.data.type == 'finger') {
        this.count--
        var index = 2 - this.count
        this.fingerList[index].url = 'data:image/png;base64,' + e.data.image
        this.fingerList[index].template = e.data.template

        if (this.count <= 0) {
          this.state = 'end'
          fclient.endMonitor()
        }
      }
    },

    close() {
      this.isShow = false
    }
  },
  computed: {
    btnText() {
      if (this.state == 'start') {
        return '开始录入'
      } else if (this.state == 'input') {
        return '正在录入'
      } else if (this.state == 'end') {
        return '确 定'
      }
    },
    msg() {
      let tt = this.temp
      return tt.replace('{0}', this.count)
    }
  },
  watch: {
    isShow(newValue) {
      if (newValue == false) {
        fclient.endMonitor()
        this.init()
      }
    }
  }
}
</script>

<style scoped>
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
  width: 500px;
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
  width: 33.3%;
  box-sizing: border-box;
  padding: 10px;
}

.finger-body img {
  width: 100%;
  height: 100%;
}

.finger-footer .finger-btn {
  float: right;
  margin-right: 10px;
  margin-bottom: 10px;
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
</style>