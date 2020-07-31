<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button type="primary" icon="minus" @click="handleDelete(selectedRowKeys)">删除</a-button>
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
    </div>
    <full-calendar :events="events" :config="config" class="test-fc" locale="fr"></full-calendar>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
  </a-card>
</template>
<script>
    import { FullCalendar } from 'vue-full-calendar'
    import moment from 'moment'
    import 'fullcalendar/dist/fullcalendar.css'
    import EditForm from './EditForm'
    const columns = [
        { title: '班次', dataIndex: 'ShiftsName', width: '10%' },
        { title: '班组', dataIndex: 'TeamTableName', width: '10%' },
        { title: '上班日期', dataIndex: 'OfficeDateText', width: '20%' },
        { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
    ]
    export default {
        components: { FullCalendar, EditForm },
        mounted() {
            this.getDataList()
        },
        data() {
            return {
                data: [],
                pagination: {
                    current: 1,
                    pageSize: 10,
                    showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`
                },
                filters: {},
                sorter: { field: 'Id', order: 'asc' },
                loading: false,
                columns,
                queryParam: {},
                selectedRowKeys: [],
                events: [
                    // { title: '赵子龙', start: '2020-04-01 12:30', end: '2020-04-01 13:30' },
                    // { title: '刘备', start: '2020-04-01T10:20', end: '2020-04-01T11:00', color: 'red' },
                    // { title: '关羽', start: '2020-04-03T14:20', end: '2020-04-03T15:20', backgroundColor: 'green' },
                    // { title: '张飞', start: '2020-04-04T16:00', end: '2020-04-04T17:00', color: 'orange', editable: true },
                    // { title: '曹操', start: '2020-04-05T18:40', end: '2020-04-05T19:20' }
                ],
                config: {
                    buttonText: { today: '今天', month: '月', week: '周', day: '日' },
                    locale: 'zh-cn',
                    editable: false, //是否允许修改事件
                    selectable: false,
                    eventLimit: 4, //事件个数
                    allDaySlot: false, //是否显示allDay
                    defaultView: 'month', //显示默认视图
                    eventClick: this.eventClick, //点击事件
                    dayClick: this.dayClick //点击日程表上面某一天
                }
            }
        },
        methods: {
            handleTableChange(pagination, filters, sorter) {
                this.pagination = { ...pagination }
                this.filters = { ...filters }
                this.sorter = { ...sorter }
                this.getDataList()
            },
            getDataList() {
                this.selectedRowKeys = []
                let dict = {}
                let index = 0
                this.loading = true
                this.$http
                    .post('/Base_Manage/Scheduling/GetDataList', {
                        PageIndex: this.pagination.current,
                        PageRows: this.pagination.pageSize,
                        SortField: this.sorter.field || 'Id',
                        SortType: this.sorter.order,
                        ...this.queryParam,
                        ...this.filters
                    })
                    .then(resJson => {
                        var backgroundList = ['orange', 'green', 'blue', 'yellowgreen', 'pink']
                        this.events = resJson.Data.map(item => {
                            if (!dict[item.ShiftsId]) {
                                dict[item.ShiftsId] = backgroundList[index++]
                            }

                            return {
                                title: item.ShiftsName + ':' + item.TeamTableName,
                                start: item.OfficeDate,
                                end: item.GoOffWork,
                                backgroundColor: dict[item.ShiftsId]
                            }
                        })
                        this.loading = false
                        this.data = resJson.Data
                        const pagination = { ...this.pagination }
                        pagination.total = resJson.Total
                        this.pagination = pagination
                    })
            },
            onSelectChange(selectedRowKeys) {
                this.selectedRowKeys = selectedRowKeys
            },
            hasSelected() {
                return this.selectedRowKeys.length > 0
            },
            hanldleAdd() {
                this.$refs.editForm.openForm()
            },
            handleEdit(id) {
                this.$refs.editForm.openForm(id)
            },
            handleDelete(ids) {
                var thisObj = this
                this.$confirm({
                    title: '确认删除吗?',
                    onOk() {
                        return new Promise((resolve, reject) => {
                            // thisObj.$http.post('/Base_Manage/Scheduling/DeleteData', { ids: JSON.stringify(ids) }).then(resJson => {
                            thisObj.$http.post('/Base_Manage/Scheduling/DeleteDataAll').then(resJson => {
                                resolve()

                                if (resJson.Success) {
                                    thisObj.$message.success('操作成功!')

                                    thisObj.getDataList()
                                } else {
                                    thisObj.$message.error(resJson.Msg)
                                }
                            })
                        })
                    }
                })
            },
            // 点击事件
            eventClick(event, jsEvent, pos) {
                console.log('eventClick', event, jsEvent, pos)
            },
            // 点击当天
            dayClick(day, jsEvent) {
                console.log('dayClick', day, jsEvent)
            }
        }
    }
</script>
