/* eslint-disable vue/require-default-prop */
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
        <a-form-item label="抄表名称" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['MeterName', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="抄表时间" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-time-picker
            @change="onChange"
            v-decorator="[
              'MeterTime',
              {
                rules: [{ required: true, message: '抄表时间!' }]
              }
            ]"
          />
        </a-form-item>
          <a-form-item label="有效时间" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-input v-decorator="['RangeTime', { rules: [{ required: true, message: '必填' }] }]"  style="width: 60px" /> 分钟以内  
        </a-form-item>
 
        
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script>
import moment from 'moment'
export default {
  props: {
    // eslint-disable-next-line vue/require-default-prop
    parentObj: Object
  },
  data () {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      visible: false,
      loading: false,
      formFields: {},
      entity: {},
      title: '',
      endOpen: false
    }
  },
  methods: {
    disabledStartDate (StartTime) {
      const EndTime = this.EndTime
      // eslint-disable-next-line no-undef
      if (!StartTime || !EndTime) {
        return false
      }
      return StartTime.valueOf() > EndTime.valueOf()
    },
    disabledEndDate (EndTime) {
      const StartTime = this.StartTime
      if (!EndTime || !StartTime) {
        return false
      }
      return StartTime.valueOf() >= EndTime.valueOf()
    },
    handleStartOpenChange (open) {
      if (!open) {
        this.endOpen = true
      }
    },
    handleEndOpenChange (open) {
      this.endOpen = open
    },
    openForm (id, title) {
      // 参数赋值
      this.title = title || '编辑表单'
      this.loading = true

      // 组件初始化
      this.init()

      // 编辑赋值
      if (id) {
        this.$nextTick(() => {
          this.formFields = this.form.getFieldsValue()

          this.$http.post('/MeterReaDing/MeterReaDingTimeSetUp/GetTheData', { id: id }).then(resJson => {
            this.entity = resJson.Data
            var setData = {}
            Object.keys(this.formFields).forEach(item => {
              setData[item] = this.entity[item]
            })
            if (setData['MeterTime']) {
              // eslint-disable-next-line no-undef
              setData['MeterTime'] = moment(setData['MeterTime'], 'HH:mm:ss') 
              
            } 
            this.form.setFieldsValue(setData)
            this.loading = false
          })
        })
      } else {
        this.loading = false
      }
    },
    init () {
      this.entity = {}
      this.form.resetFields()
      this.visible = true
    },
    handleSubmit () {
      this.form.validateFields((errors, values) => {
        // 校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())
          if (this.entity['MeterTime']) {
            this.entity['MeterTime'] = this.entity['MeterTime'].format('HH:mm:ss')
          }
          this.loading = true
          this.$http.post('/MeterReaDing/MeterReaDingTimeSetUp/SaveData', this.entity).then(resJson => {
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
    handleCancel () {
      this.visible = false
    }
  }
}
</script>
