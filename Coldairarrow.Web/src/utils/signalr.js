import setting from '../config/defaultSettings';
import * as signalR from '@microsoft/signalr';
 
var connection = new signalR.HubConnectionBuilder()
    .withUrl(setting.publishRootUrl+'/RemoteHub')
    .configureLogging(signalR.LogLevel.Information)
    .build()

connection.on('DeviceData', function (obj) {
    console.log(obj)
})

connection
    .start()
    .then(function () {
        console.log('连接成功')
    })
    .catch(function (ex) {
        console.log('连接失败' + ex)
        //SignalR JavaScript 客户端不会自动重新连接，必须编写代码将手动重新连接你的客户端
        setTimeout(() => start(), 5000)
    })

async function start() {
    try {
        await connection.start()
        console.log('connected')
    } catch (err) {
        console.log(err)
        setTimeout(() => start(), 5000)
    }
}

connection.onclose(async () => {
    start()
})

//绑定事件("ReceiveMessage"和服务器端的SendMessage方法中的第一个参数一致)
//服务器端调用客户端
connection.on('ReceiveMessage', function (data) {
    // alert("返回");
    console.log(data)
})

//调用服务器端
export function sendMessage(methodName, data) {
    alert(11)
    connection.invoke(methodName, data).catch(err => console.error("发送失败：" + err.toString()));
}

export function acceptMessage(methodName, callback) {
    connection.on(methodName, callback);
}

export default {
    sendMessage,
    acceptMessage
}