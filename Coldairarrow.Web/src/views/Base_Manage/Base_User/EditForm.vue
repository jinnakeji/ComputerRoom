<template>
  <div>
    <a-modal
      title="编辑表单"
      width="40%"
      :visible="visible"
      :confirmLoading="confirmLoading"
      @ok="handleSubmit"
      @cancel="handleCancel"
    >
      <a-spin :spinning="confirmLoading">
        <a-form :form="form">
          <a-form-item label="用户名" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-input
              v-decorator="['UserName', { rules: [{ required: true, message: '请输入用户名' }] }]"
            />
          </a-form-item>
          <a-form-item label="密码" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-input
              type="password"
              autocomplete="false"
              placeholder="请输入密码"
              v-decorator="['newPwd', { rules: [{ required: false, message: '请输入密码' }] }]"
            ></a-input>
          </a-form-item>
          <a-form-item label="姓名" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-input
              v-decorator="['RealName', { rules: [{ required: true, message: '请输入用户名' }] }]"
            />
          </a-form-item>
          <a-form-item label="性别" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-radio-group v-decorator="['Sex', { rules: [{ required: true }], initialValue: 0 }]">
              <a-radio :value="0">女</a-radio>
              <a-radio :value="1">男</a-radio>
            </a-radio-group>
          </a-form-item>
          <a-form-item label="出生日期" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-date-picker
              style="width: 300px"
              format="YYYY-MM-DD"
              v-decorator="['Birthday', { rules: [{ required: false }] }]"
            />
          </a-form-item>
          <a-form-item label="所属部门" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-tree-select
              allowClear
              style="width: 300px"
              :dropdownStyle="{ maxHeight: '400px', overflow: 'auto' }"
              :treeData="DepartmentIdTreeData"
              placeholder="请选择部门"
              treeDefaultExpandAll
              v-decorator="['DepartmentId', { rules: [{ required: false }] }]"
            ></a-tree-select>
          </a-form-item>
          <a-form-item label="角色" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-select
              allowClear
              mode="multiple"
              v-decorator="['RoleIdList', { rules: [{ required: false }] }]"
            >
              <a-select-option v-for="item in RoleOptionList" :key="item.Id">{{ item.RoleName }}</a-select-option>
            </a-select>
          </a-form-item>

          <a-form-item label="班组" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-select allowClear v-decorator="['TeamTableId', { rules: [{ required: false }] }]">
              <a-select-option v-for="item in TeamTableList" :key="item.Id">{{ item.TeamTableName }}</a-select-option>
            </a-select>
          </a-form-item>

          <a-form-item label="指纹" :labelCol="labelCol" :wrapperCol="wrapperCol">
            <a-button @click="btnFingerInput">{{ entity.FingerTemplate ? '修改指纹' : '录入指纹' }}</a-button>
          </a-form-item>
        </a-form>
      </a-spin>
    </a-modal>
    <finger-input ref="fingerInput" :userId="entity.Id" @inputCompleted="inputCompleted"></finger-input>
  </div>
</template>

<script>
import FingerInput from '../../../components/FingerInput'
import moment from 'moment'
export default {
  components: { FingerInput },
  props: {
    afterSubmit: {
      type: Function,
      default: null
    }
  },
  data() {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      visible: false,
      confirmLoading: false,
      formFields: {},
      entity: { Id: '' },
      DepartmentIdTreeData: [],
      RoleOptionList: [],
      TeamTableList: []
    }
  },
  methods: {
    add() {
      this.entity = {}
      this.visible = true
      this.form.resetFields()
      this.init()
    },
    edit(id) {
      this.visible = true

      this.$nextTick(() => {
        this.formFields = this.form.getFieldsValue()

        this.$http.post('/Base_Manage/Base_User/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data
          var setData = {}
          Object.keys(this.formFields).forEach(item => {
            setData[item] = this.entity[item]
          })
          if (setData['Birthday']) {
            setData['Birthday'] = moment(setData['Birthday'])
          }
          this.form.setFieldsValue(setData)
          this.init()
        })
      })
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())
          this.entity['roleIdsJson'] = JSON.stringify(this.entity['RoleIdList'])
          this.entity['tempIdsJson'] = JSON.stringify(this.entity['TeamIdList'])
          if (this.entity['Birthday']) {
            this.entity['Birthday'] = this.entity['Birthday'].format('YYYY-MM-DD')
          }
          this.confirmLoading = true
          this.$http.post('/Base_Manage/Base_User/SaveData', this.entity).then(resJson => {
            this.confirmLoading = false

            if (resJson.Success) {
              this.$message.success('操作成功!')
              this.afterSubmit()
              this.visible = false
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
    init() {
      this.$http.post('/Base_Manage/Base_Department/GetTreeDataList').then(resJson => {
        if (resJson.Success) {
          this.DepartmentIdTreeData = resJson.Data
        }
      })
      this.$http.post('/Base_Manage/Base_Role/GetDataList').then(resJson => {
        if (resJson.Success) {
          this.RoleOptionList = resJson.Data
        }
      })

      this.$http.post('/Base_Manage/TeamTable/GetDataList').then(resJson => {
        if (resJson.Success) {
          this.TeamTableList = resJson.Data
        }
      })
    },
    btnFingerInput() {
      this.$refs.fingerInput.isShow = true
    },
    inputCompleted(res) {
      this.$refs.fingerInput.isShow = false
      if (res.type == 'success') {
        this.$success({
          title: '消息',
          content: '指纹录入成功'
        })
        this.entity.FingerTemplate = e.template
      } else {
        this.$error({
          title: '消息',
          content: res.message
        })
      }
    }
  }
}
</script>
