<template>
  <a-card :bordered="false">
    <!-- <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button
        type="primary"
        icon="minus"
        @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
      >
        删除
      </a-button>
    </div> -->

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="20">
          <a-col :md="6" :sm="24">
            <a-form-item label="内容">
              <a-input v-model="queryParam.logContent" placeholder="" />
            </a-form-item>
          </a-col>
          <a-col :md="3" :sm="24">
            <a-form-item label="级别">
              <a-select v-model="queryParam.level" allowClear>
                <a-select-option v-for="item in LoglevelList" :key="item.text">{{ item.text }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="类别">
              <a-select v-model="queryParam.logType" allowClear>
                <a-select-option v-for="item in LogTypeList" :key="item.text">{{ item.text }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="操作人">
              <a-input v-model="queryParam.opUserName" placeholder="" />
            </a-form-item>
          </a-col>
          <a-col :md="10" :sm="24">
            <a-form-item label="时间">
              <a-date-picker v-model="queryParam.startTime" showTime format="YYYY-MM-DD HH:mm:ss" />
              ~<a-date-picker v-model="queryParam.endTime" showTime format="YYYY-MM-DD HH:mm:ss" />
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
      :bordered="true"
      :rowClassName="rowClassName"
      size="small"
    >
      <span slot="LogContent" slot-scope="value">
        <template v-for="item in (value || '').replace(/\r\n/g, '\n').split('\n')">
          {{ item }}
          <br/>
        </template>
      </span>
    </a-table>
  </a-card>
</template>

<script>
import moment from 'moment'

const columns = [
  { title: '内容', dataIndex: 'LogContent', width: '50%', scopedSlots: { customRender: 'LogContent' } },
  { title: '级别', dataIndex: 'Level', width: '5%' },
  { title: '类别', dataIndex: 'LogType', width: '10%' },
  { title: '操作人', dataIndex: 'CreatorRealName', width: '10%' },
  { title: '时间', dataIndex: 'CreateTime', width: '15%' }
]

export default {
  mounted() {
    this.init()
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
      visible: false,
      LogTypeList: [],
      LoglevelList: []
    }
  },
  methods: {
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    init() {
      this.$http.post('/Base_Manage/Base_Log/GetLogTypeList').then(resJson => {
        this.LogTypeList = resJson.Data
      })
      this.$http.post('/Base_Manage/Base_Log/GetLoglevelList').then(resJson => {
        this.LoglevelList = resJson.Data
      })
    },
    getDataList() {
      this.loading = true
      this.$http
        .post('/Base_Manage/Base_Log/GetLogList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: 'CreateTime',
          SortType: 'desc',
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
    rowClassName(row, index) {
      var level = row['Level']
      if (level == 'Warn') {
        return 'yellow'
      } else if (level == 'Error' || level == 'Fatal') {
        return 'red'
      }
    }
  }
}
</script>
<style>
.yellow {
  color: #ff7f00;
}
.red {
  color: #ed5565;
}
</style>