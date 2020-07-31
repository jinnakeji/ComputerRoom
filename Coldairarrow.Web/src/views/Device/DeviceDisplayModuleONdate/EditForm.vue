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
        <a-form-item label="数据模块" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select v-decorator="['DeviceDisplayModuleId', { rules: [{ required: true, message: '必填' }] }]">
            <a-select-option v-for="item in DeviceDisplayModuleData" :key="item.Id" :value="item.Id">{{
              item.DeviceDisplayModuleName
            }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="是否显示" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <!-- <a-input v-decorator="['IsDisplay', { rules: [{ required: true, message: '必填' }] }]" /> -->
          <a-select v-decorator="['IsDisplay', { rules: [{ required: true, message: '必填' }] }]">
            <a-select-option :value="1">是</a-select-option>
            <a-select-option :value="0">否</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="部门" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-tree-select
            style="width: 300px"
            allowClear
            :dropdownStyle="{ maxHeight: '400px', overflow: 'auto' }"
            :treeData="ParentIdTreeData"
            placeholder="请选择部门"
            treeDefaultExpandAll
            v-decorator="['DepartmentId', { rules: [{ required: false }] }]"
          ></a-tree-select>
        </a-form-item>
        <a-form-item label="站点名称" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select v-decorator="['DeviceCode', { rules: [{ required: true, message: '必填' }] }]">
            <a-select-option v-for="item in Macs" :key="item.Id" :value="item.Id" @click="setDevice(item.DeviceCode)">{{
              item.DeviceCode
            }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="设备" :labelCol="labelCol" :wrapperCol="wrapperCol">
          
          <!-- <a-input v-decorator="['DeviceId', { rules: [{ required: true, message: '必填' }] }]" /> -->
          
          <a-select v-decorator="['deviceid', { rules: [{ required: true, message: '必填' }] }]">
            <a-select-option v-for="item in DeviceMacData" :key="item.Id" :value="item.Id">{{
              item.Name
            }}</a-select-option>
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
      ParentIdTreeData: [],
      DeviceDisplayModuleData: [],
      DeviceData:[],
      DeviceMacData:[],
      Macs:[]
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

          this.$http.post('/Device/DeviceDisplayModuleONdate/GetTheData', { id: id }).then(resJson => {
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
      this.Macs=[]
      this.visible = true 
      this.$http.post('/Base_Manage/Base_Department/GetTreeDataList').then(resJson => {
        
        if (resJson.Success) {
          this.ParentIdTreeData = resJson.Data
          console.log(this.ParentIdTreeData);
          
        }
      })
      this.$http.post('/Device/DeviceDisplayModule/GetDataList').then(resJson => {
        if (resJson.Success) {
          this.DeviceDisplayModuleData = resJson.Data
          console.log(this.DeviceDisplayModuleData);
          
        }
      })
      this.$http.post('/Device/T_Device/GetDataList').then(resJson => {
        if (resJson.Success) {                 
          this.DeviceData = resJson.Data
          this.DeviceMacData=this.DeviceData 

          var macs=_.groupBy(this.DeviceData, 'DeviceCode')//deviceid DeviceMacCode
          _.each(macs,(mac)=>{
            this.Macs.push(mac[0])
          }) 
          console.log(this.Macs)      
        }
      })
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          this.loading = true
          this.$http.post('/Device/DeviceDisplayModuleONdate/SaveData', this.entity).then(resJson => {
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
    },
    setDevice(mac){   
      this.form.setFieldsValue({"deviceid":""})
      this.DeviceMacData=this.DeviceData.filter(t=> t.DeviceCode==mac)   
    }
  }
}
</script>
