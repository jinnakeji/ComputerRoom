<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-spin :spinning="loading">
      <a-form :form="form">
        <a-form-item label="设备类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select
            @change="deviceChange"
            v-decorator="['DeviceTypeId', { rules: [{ required: true, message: '必填' }] }]"
          >
            <a-select-option
              v-for="item in deviceList"
              :key="item.Id"
              :value="item.Id"
            >{{ item.DisplayName || item.Name }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="属性" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select v-decorator="['PropId', { rules: [{ required: true, message: '必填' }] }]">
            <a-select-option
              v-for="item in propList"
              :key="item.Id"
              :value="item.Id"
            >{{ item.DisplayName || item.Name }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="是否显示" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select v-decorator="['IsShow', { rules: [{ required: true, message: '必填' }] }]">
            <a-select-option
              v-for="item in showLIst"
              :key="item.Id"
              :value="item.Value"
            >{{ item.Name }}</a-select-option>
          </a-select>
        </a-form-item>
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    parentObj: Object
  },
  data() {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      visible: false,
      loading: false,
      formFields: {},
      entity: {},
      title: '',
      deviceList: [],
      propList: [],
      showLIst: [
        {
          Id: 1,
          Value: true,
          Name: '是'
        },
        {
          Id: 2,
          Value: false,
          Name: '否'
        }
      ]
    }
  },
  methods: {
    openForm(id, title) {
      //参数赋值
      this.title = title || '编辑表单'
      this.loading = true

      //组件初始化
      this.init()

      //编辑赋值
      if (id) {
        this.$nextTick(() => {
          this.formFields = this.form.getFieldsValue()

          this.$http.post('/Device/T_ShowDeviceProp/GetTheData', { id: id }).then(resJson => {
            this.entity = resJson.Data
            var setData = {}
            Object.keys(this.formFields).forEach(item => {
              setData[item] = this.entity[item]
            })
            this.form.setFieldsValue(setData)
            this.loading = false
          })
        })
      } else {
        this.loading = false
      }
    },
    init() {
      this.entity = {}
      this.form.resetFields()
      this.visible = true

      this.$http.post('/Device/T_DeviceType/GetDataViewListAll').then(resJson => {
        if (resJson.Success) {
          this.deviceList = resJson.Data
        }
      })
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          this.loading = true
          this.$http.post('/Device/T_ShowDeviceProp/SaveData', this.entity).then(resJson => {
            this.loading = false

            if (resJson.Success) {
              this.$message.success('操作成功!')
              this.visible = false

              this.parentObj.getDataList()
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        }
      })
    },
    deviceChange(value) {
      debugger
      this.$http.get('/Device/T_DeviceProp/GetDataByDeviceTypeId', { params: { deviceTypeId: value } }).then(resJson => {
        this.propList = resJson.Data
      })
    },
    handleCancel() {
      this.visible = false
    }
  }
}
</script>
