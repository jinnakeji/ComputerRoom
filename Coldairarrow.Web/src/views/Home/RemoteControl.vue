<template>
  <div class="line-box" style="height: 150px;">
    <table class="border-table" style="height: 100%;">
      <tr>
        <td rowspan="3" width="114px">
          <img src="../../assets/switch.png" />
        </td>
        <td>开关名称</td>
        <td width="80px" v-for="item in switchList" :key="item.index">{{ item.name }}</td>
      </tr>
      <tr>
        <td>当前状态</td>
        <td v-for="item in switchList" :key="item.index">
          <span
            v-if="item.value == 1 || item.value == 0"
            :class="{ switch: true, open: item.value == 1 }"
          >{{ item.value == 1 ? '开' : '关' }}</span>
        </td>
      </tr>
      <tr>
        <td>开关控制</td>
        <td v-for="item in switchList" :key="item.index">
          <a-switch
            v-if="item.has"
            checkedChildren="开"
            unCheckedChildren="关"
            :defaultChecked="item.value == 1"
          />
        </td>
      </tr>
    </table>
  </div>
</template>
<script>
    export default {}
</script>

<style scoped>
</style>