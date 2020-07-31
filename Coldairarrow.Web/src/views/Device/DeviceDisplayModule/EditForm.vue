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
                <a-form-item label="模块名称" :labelCol="labelCol" :wrapperCol="wrapperCol">
                    <a-input
                        v-decorator="['DeviceDisplayModuleName', { rules: [{ required: true, message: '必填' }] }]"
                    />
                </a-form-item>
                <a-form-item label="首页位置" :labelCol="labelCol" :wrapperCol="wrapperCol">
                    <a-select v-decorator="['IsDisplay', { rules: [{ required: true, message: '必填' }] }]">
                        <a-select-option :value="1">1</a-select-option>
                        <a-select-option :value="2">2</a-select-option>
                        <a-select-option :value="3">3</a-select-option>
                    </a-select>
                </a-form-item>
                <a-form-item label="部门" :labelCol="labelCol" :wrapperCol="wrapperCol" v-show="false">
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
                <a-form-item label="排序" :labelCol="labelCol" :wrapperCol="wrapperCol">
                    <a-input v-decorator="['Sort', { rules: [{ required: true, message: '必填' }] }]" />
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
            ParentIdTreeData: []
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

                    this.$http.post('/Device/DeviceDisplayModule/GetTheData', { id: id }).then(resJson => {
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
            this.$http.post('/Base_Manage/Base_Department/GetTreeDataList').then(resJson => {
                if (resJson.Success) {
                    this.ParentIdTreeData = resJson.Data
                }
            })
        },
        handleSubmit() {
            this.form.validateFields((errors, values) => {
                //校验成功
                if (!errors) {
                    this.entity = Object.assign(this.entity, this.form.getFieldsValue())

                    this.loading = true
                    this.$http.post('/Device/DeviceDisplayModule/SaveData', this.entity).then(resJson => {
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
