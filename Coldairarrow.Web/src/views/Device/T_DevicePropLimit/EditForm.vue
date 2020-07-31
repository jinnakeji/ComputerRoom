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
        <a-form-item label="设备" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input readonly v-model="entity.DeviceName" />
        </a-form-item>
        <a-form-item label="属性" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input readonly v-model="entity.PropName" />
        </a-form-item>
        <a-form-item label="最大值" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Max']" />
        </a-form-item>
        <a-form-item label="最小值" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Min']" />
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
      formFields: {
        DeviceName: '',
        PropName: '',
        DeviceId: 0,
        PropId: 0
      },
      entity: {},
      title: '',
      deviceList: [],
      propList: []
    }
  },
  methods: {
    openForm(record, title) {
      //参数赋值
      this.title = title || '编辑表单'
      this.loading = true

      //组件初始化
      this.init()

      this.entity.DeviceId = record.DeviceId
      this.entity.PropId = record.PropId
      this.entity.DeviceName = record.DeviceName
      this.entity.PropName = record.PropName

      //编辑赋值
      if (record.LimitId) {
        this.$nextTick(() => {
          this.formFields = this.form.getFieldsValue()

          this.$http
            .post('/Device/T_DevicePropLimit/GetTheData', {
              deviceId: record.DeviceId,
              number: record.Number,
              propId: record.PropId
            })
            .then(resJson => {
              this.entity.Max = resJson.Data.Max
              this.entity.Min = resJson.Data.Min
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
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          var aa = this
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())
          debugger
          this.loading = true
          this.$http.post('/Device/T_DevicePropLimit/SaveData', this.entity).then(resJson => {
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
    handleCancel() {
      this.visible = false
    }
  }
}
</script>
