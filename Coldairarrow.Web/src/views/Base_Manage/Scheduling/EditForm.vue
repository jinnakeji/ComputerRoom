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
                <a-form-item label="班次" :labelCol="labelCol" :wrapperCol="wrapperCol">
                    <!-- <a-input v-decorator="['ShiftsId', { rules: [{ required: true, message: '必填' }] }]" /> -->
                    <a-select allowClear mode="multiple" v-decorator="['ShiftsId', { rules: [{ required: false }] }]">
                        <a-select-option v-for="item in ShiftsList" :key="item.Id">{{
                            item.ShiftsName
                        }}</a-select-option>
                    </a-select>
                </a-form-item>
                <a-form-item label="班组" :labelCol="labelCol" :wrapperCol="wrapperCol">
                    <a-select
                        allowClear
                        mode="multiple"
                        v-decorator="['TeamTableId', { rules: [{ required: false }] }]"
                    >
                        <a-select-option v-for="item in TeamTableList" :key="item.Id">{{
                            item.TeamTableName
                        }}</a-select-option>
                    </a-select>
                </a-form-item>
                <a-form-item label="日期" :labelCol="labelCol" :wrapperCol="wrapperCol">
                    <!-- <a-input v-decorator="['OfficeDate', { rules: [{ required: true, message: '必填' }] }]" /> -->
                    <a-range-picker v-decorator="['OnOffDate', { rules: [{ required: true, message: '必填' }] }]" />
                    <br />
                </a-form-item>
                <a-form-item label="过滤日期" :labelCol="labelCol" :wrapperCol="wrapperCol">
                    <a-checkbox-group @change="onChange" v-decorator="['strRestDay']">
                        <a-row>
                            <a-col :span="12">
                                <a-checkbox value="6">周六</a-checkbox>
                            </a-col>
                            <a-col :span="12">
                                <a-checkbox value="7">周日</a-checkbox>
                            </a-col>
                        </a-row>
                    </a-checkbox-group>
                </a-form-item>
            </a-form>
        </a-spin>
    </a-modal>
</template>

<script>
import { times } from 'underscore'
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
            TeamTableList: [],
            ShiftsList: []
        }
    },
    methods: {
        onChange(checkedValues) {
            console.log('checked = ', checkedValues)
        },
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

                    this.$http.post('/Base_Manage/Scheduling/GetTheData', { id: id }).then(resJson => {
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

            this.$http.post('/Base_Manage/TeamTable/GetDataList').then(resJson => {
                if (resJson.Success) {
                    this.TeamTableList = resJson.Data
                }
            })
            this.$http.post('/Base_Manage/Shifts/GetDataList').then(resJson => {
                if (resJson.Success) {
                    this.ShiftsList = resJson.Data
                }
            })
        },
        handleSubmit() {
            this.form.validateFields((errors, values) => {
                //校验成功
                if (!errors) {
                    debugger
                    var self = this
                    this.entity = Object.assign(this.entity, this.form.getFieldsValue())
                    if (this.entity['OnOffDate']) {
                        var item1 = this.entity['OnOffDate'][0].format('YYYY-MM-DD')
                        var item2 = this.entity['OnOffDate'][1].format('YYYY-MM-DD')
                        this.entity['OnOffDate'] = [item1, item2]
                    }
                    this.loading = true
                    this.$http.post('/Base_Manage/Scheduling/SaveData', this.entity).then(resJson => {
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
