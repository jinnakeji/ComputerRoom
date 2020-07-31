<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="查询类别">
              <a-select allowClear v-model="queryParam.condition">
                 <a-select-option value="DeviceTypeName">设备类别</a-select-option>
                 <a-select-option value="PropName">属性名称</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item>
              <a-input v-model="queryParam.keyword" placeholder="关键字" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="getDataList">查询</a-button>
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table
      ref="table"
      :columns="columns"
      :rowKey="row => row.Id"
      :dataSource="data"
      :pagination="pagination"
      :loading="loading"
      @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true"
      size="small"
    >
      <span slot="show" slot-scope="text,record">
        <a-switch
          checked-children="显示"
          un-checked-children="隐藏"
          :default-checked="record.IsShow"
          @change="change(record)"
        />
      </span>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
  </a-card>
</template>

<script>
    import EditForm from './EditForm'

    const columns = [
        { title: '设备类型', dataIndex: 'DeviceTypeName', width: '20%' },
        { title: '设备属性', dataIndex: 'PropName', width: '20%' },
        { title: '显示', dataIndex: 'IsShow', scopedSlots: { customRender: 'show' } }
    ]

    export default {
        components: {
            EditForm
        },
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
                selectedRowKeys: []
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

                this.loading = true
                this.$http
                    .post('/Device/T_ShowDeviceProp/GetDataList', {
                        PageIndex: this.pagination.current,
                        PageRows: this.pagination.pageSize,
                        SortField: this.sorter.field || 'Id',
                        SortType: this.sorter.order,
                        ...this.queryParam,
                        ...this.filters
                    })
                    .then(resJson => {
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
            change(record) {
                this.$http
                    .post('/Device/T_ShowDeviceProp/SaveData', {
                        DeviceTypeId: record.DeviceTypeId,
                        PropId: record.PropId,
                        IsShow: !record.IsShow
                    })
                    .then(res => {
                        debugger;
                    })
            }
        }
    }
</script>