<template>
  <div class="main">
    <a-spin :spinning="loading">
      <a-form
        id="formLogin"
        class="user-layout-login"
        ref="formLogin"
        :form="form"
        @submit="handleSubmit"
      >
        <a-tabs
          :activeKey="customActiveKey"
          :tabBarStyle="{ textAlign: 'center', borderBottom: 'unset' }"
          @change="handleTabClick"
        >
          <a-tab-pane key="account" tab="账号密码登录">
            <a-form-item>
              <a-input
                size="large"
                type="text"
                placeholder="请输入用户名"
                v-decorator="['userName', { rules: [{ required: true, message: '请输入用户名' }] }]"
              >
                <a-icon slot="prefix" type="user" :style="{ color: 'rgba(0,0,0,.25)' }" />
              </a-input>
            </a-form-item>

            <a-form-item>
              <a-input
                size="large"
                type="password"
                autocomplete="false"
                placeholder="请输入密码"
                v-decorator="['password', { rules: [{ required: true, message: '请输入密码' }] }]"
              >
                <a-icon slot="prefix" type="lock" :style="{ color: 'rgba(0,0,0,.25)' }" />
              </a-input>
            </a-form-item>
            <a-form-item>
              <a-checkbox v-decorator="['savePwd', { valuePropName: 'checked' }]">记住密码</a-checkbox>
            </a-form-item>
          </a-tab-pane>
          <a-tab-pane key="finger" tab="指纹登陆">
            <div class="finger">
              <img :src="fingerImg" />
            </div>
          </a-tab-pane>
        </a-tabs>

        <a-form-item style="margin-top:24px">
          <a-button size="large" type="primary" htmlType="submit" class="login-button">{{ btnText }}</a-button>
        </a-form-item>
      </a-form>
    </a-spin>
  </div>
</template>

<script>
import TokenCache from '@/utils/cache/TokenCache'
import FClient from '../../components/_util/fingerclient'
import fingerImg from '../../assets/finger.gif'
var fclient = new FClient()
export default {
  data() {
    return {
      btnText: '登录',
      url: 'ws://localhost:8055',
      loading: false,
      customActiveKey: 'account',
      form: this.$form.createForm(this),
      fingerTemplate: '',
      fingerImg: fingerImg
    }
  },
  mounted() {
    var userName = localStorage.getItem('userName')
    var password = localStorage.getItem('password')
    if (userName && password) {
      this.form.setFieldsValue({ userName, password, savePwd: true })
    }

    fclient.connectionServer(this.url)
    fclient.on('message', this.message.bind(this))
  },
  methods: {
    handleTabClick(key) {
      debugger
      this.customActiveKey = key
      if (key == 'account') {
        this.btnText = '登陆'
        fclient.endMonitor()
      } else if (key == 'finger') {
        this.btnText = '点击录入指纹'
      }
    },
    message(e) {
      if (e.data.type == 'finger') {
        this.fingerTemplate = e.data.template
        this.loading = true
        this.$http
          .post('/api/Finger/Identity', {
            template: this.fingerTemplate
          })
          .then(res => {
            this.loading = false
            if (res.Success) {
              fclient.endMonitor()
              TokenCache.setToken(res.Data)
              this.$router.push({ path: '/' })
            }
          })
      }
    },
    handleSubmit(e) {
      e.preventDefault()

      if (this.customActiveKey == 'finger') {
        fclient.startMonitor()
        this.btnText = '请按指纹登陆'
        return
      }

      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          var values = this.form.getFieldsValue()
          this.loading = true
          this.$http.post('/Base_Manage/Home/SubmitLogin', values).then(resJson => {
            this.loading = false

            if (resJson.Success) {
              TokenCache.setToken(resJson.Data)
              //保存密码
              if (values['savePwd']) {
                localStorage.setItem('userName', values['userName'])
                localStorage.setItem('password', values['password'])
              } else {
                localStorage.removeItem('userName')
                localStorage.removeItem('password')
              }
              this.$router.push({ path: '/' })
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        }
      })
    }
  }
}
</script>

<style lang="less" scoped>
.user-layout-login {
  label {
    font-size: 14px;
  }

  .getCaptcha {
    display: block;
    width: 100%;
    height: 40px;
  }

  .forge-password {
    font-size: 14px;
  }

  button.login-button {
    padding: 0 15px;
    font-size: 16px;
    height: 40px;
    width: 100%;
    background: #1DA57A;
    border-color: #1DA57A;
    text-shadow: 0 -1px 0 rgba(0, 0, 0, 0.12);
    -webkit-box-shadow: 0 2px 0 rgba(0, 0, 0, 0.045);
    box-shadow: 0 2px 0 rgba(0, 0, 0, 0.045);
  }

  .user-login-other {
    text-align: left;
    margin-top: 24px;
    line-height: 22px;

    .item-icon {
      font-size: 24px;
      color: rgba(0, 0, 0, 0.2);
      margin-left: 16px;
      vertical-align: middle;
      cursor: pointer;
      transition: color 0.3s;

      &:hover {
        color: #1890ff;
      }
    }

    .register {
      float: right;
    }
  }

  .finger {
    width: 368px;
    height: 192px;
    background: white;
  }

  .finger img {
    width: 66%;
    height: 100%;
    margin: auto;
    display: block;
  }
}
</style>
