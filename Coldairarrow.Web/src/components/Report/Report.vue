<template>
  <div class="report" v-if="isShow">
    <div class="bg"></div>
    <a-icon type="close-circle" class="my-close" @click="close" />
    <div class="context">
      <table>
        <tr v-for="(item,index) in datas" :key="index">
          <template v-if="item.key == 'title'">
            <td class="title" width="16%">抄表时间段：</td>
            <td class="title" width="16%">{{ item.date }}</td>
            <td class="title" width="16%">实际抄表时间：</td>
            <td class="title" width="16%">{{ item.time }}</td>
            <td class="title" width="16%">抄表人：</td>
            <td class="title" width="16%">{{ item.userName }}</td>
          </template>
          <template v-else-if="item.key == 'module'">
            <td class="module" colspan="6">{{ item.module }}</td>
          </template>
          <template v-else-if="item.key == 'data'">
            <td>{{ item.deviceName }}</td>
            <td colspan="5">{{ item.MeterReaDingOnDutyData}}</td>
          </template>
          <template v-else-if="item.key == 'empty'">
            <td colspan="6" class="empty"></td>
          </template>
        </tr>

        <tr>
          <td>备注</td>
          <td colspan="5" style="padding:0">
            <textarea v-model="text"></textarea>
          </td>
        </tr>
        <tr>
          <td colspan="6">
            <a-button class="right" type="primary" @click="submit">提交</a-button>
          </td>
        </tr>
      </table>
    </div>
  </div>
</template>

<script>
    import _ from 'underscore'

    export default {
        props: {
            id: {
                type: String,
                default: ''
            },
            isShow: {
                type: Boolean,
                default: false
            }
        },
        data() {
            return {
                datas: [],
                text: ''
            }
        },

        destroyed() {
            this.overflowHidden(false)
        },
        methods: {
            close() {
                this.overflowHidden(false)
                this.isShow = false
                this.$emit('update:is-show', false)
            },
            overflowHidden(b) {
                if (b) {
                    document.documentElement.classList.add('overflow-hidden')
                } else {
                    document.documentElement.classList.remove('overflow-hidden')
                }
            },
            getData(deptId) {
                debugger
                this.$http
                    .post('/MeterReaDing/MeterReaDingOnDuty/GetReportTable', { deptId })
                    .then(res => {
                        var arr = []

                        var g1 = _.groupBy(res.Data, item => item.CreateTime)
                        for (var val in g1) {
                            arr.push({
                                key: 'title',
                                date: val.split(' ')[0],
                                time: val.split(' ')[1],
                                userName: g1[val][0].RealName
                            })

                            var item = g1[val]
                            var g2 = _.groupBy(item, aa => aa.moduleName)
                            for (var val in g2) {
                                arr.push({ key: 'module', module: val })
                                for (let i = 0; i < g2[val].length; i++) {
                                    let item = g2[val][i]
                                    item.key = 'data'
                                    arr.push(item)
                                }
                            }

                            arr.push({ key: 'empty' })
                        }

                        this.datas = arr

                        this.overflowHidden(true)
                    })
                    .catch(e => {
                        debugger
                    })
            },
            submit() {
                this.$emit('submit', this.text)
            }
        },
        watch: {
            isShow(newValue) {
                this.overflowHidden(newValue)
            }
        }
    }
</script>

<style lang="less" scoped>
    * {
        color: black;
    }
    .report {
        z-index: 10;
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;

        table {
            margin: 20px auto;
            position: relative;
            z-index: 10;
            width: 90%;
            border-collapse: collapse;
            // height: 1000px;
            overflow-y: auto;

            th,
            td {
                border: 1px solid gray;
                padding: 4px;
            }
        }
    }

    .context {
        overflow-y: auto;
        position: absolute;
        height: 100%;
        width: 100%;
    }

    caption {
        caption-side: top;
        color: white;
        text-align: center;
        background-color: #515151;
        font-size: 20px;
        padding-top: 10px;
    }

    .bg {
        position: absolute;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        background-color: white;
        // opacity: 0.8;
    }

    html {
        overflow: hidden;
    }

    .my-close {
        z-index: 12;
        position: absolute;
        font-size: 30px;
        right: 30px;
        top: 10px;
        cursor: pointer;
    }

    textarea {
        background-color: transparent;
        width: 100%;
        height: 100px;
        border: 0;
        outline-width: 0;
    }

    .right {
        float: right;
        margin-right: 2px;
    }

    .title {
        background-color: yellowgreen;
    }

    .module {
        background-color: gray;
    }

    .empty {
        border-left: 0 !important;
        border-right: 0 !important;
        height: 40px;
    }
</style>
