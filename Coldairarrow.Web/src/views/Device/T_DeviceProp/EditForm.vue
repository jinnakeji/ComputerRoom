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
        <a-form-item label="属性名" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Name', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
           <a-form-item label="中文属性名" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['ProName', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="属性类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select v-decorator="['TypeId', { rules: [{ required: true, message: '必填' }] }]">
            <a-select-option v-for="item in types" :key="item.Id" :value="item.Id">
              {{
              item.Name
              }}
            </a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="属性单位" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Unit', { rules: [ ] }]" />
        </a-form-item>
        
        <a-form-item label="设备类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select v-decorator="['DeviceTypeId', { rules: [{ required: true, message: '必填' }] }]">
            <a-select-option
              v-for="item in DeviceType"
              :key="item.Id"
              :value="item.Id"
            >{{item.DisplayName}}</a-select-option>
          </a-select>
        </a-form-item>
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script>
    import _ from 'underscore'
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
                DeviceType: []
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

                        this.$http.post('/Device/T_DeviceProp/GetTheData', { id: id }).then(resJson => {
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
                this.$http.post('/Device/T_Type/GetDataList').then(resJson => {
                    if (resJson.Success) {
                        this.types = resJson.Data
                    }
                })
                this.$http.post('/Device/T_DeviceType/GetDataList').then(resJson => {
                    if (resJson.Success) {
                        //this.DeviceData = resJson.Data
                        this.DeviceType = resJson.Data

                        // var types=_.groupBy(this.DeviceData, 'DeviceTypeId')
                        // _.each(types,(dts)=>{
                        //   this.DeviceType.push(dts[0])
                        // })
                    }
                })
            },
            handleSubmit() {
                this.form.validateFields((errors, values) => {
                    //校验成功
                    if (!errors) {
                        this.entity = Object.assign(this.entity, this.form.getFieldsValue())
                        debugger
                        this.loading = true
                        this.$http.post('/Device/T_DeviceProp/SaveData', this.entity).then(resJson => {
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
            // setDevicData(DeviceTypeId){
            //   this.form.setFieldsValue({"DeviceId":""})
            //   this.DeviceDataByType=this.DeviceData.filter(t=> t.DeviceTypeId==DeviceTypeId)
            // }
        }
    }
</script>
