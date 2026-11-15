using MQTTnet;
using MQTTnet.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace khongkhi
{
    public class MqttListenerService
    {
        private IMqttClient _mqttClient;
        private ThuNhanService _thuNhanService = new ThuNhanService();

        // Bổ sung tham số Action<string> onMessageReceived vào đây
        public async Task StartListeningAsync(Action<string> onMessageReceived = null)
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.hivemq.com", 1883) // Khớp với Wokwi của bạn
                .Build();

            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                var payloadJson = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                // Gọi service xử lý và ghi thẳng vào Oracle
                _thuNhanService.XuLyBanTinTuWokwi(payloadJson);

                // Thông báo cho giao diện load lại dữ liệu nếu cần
                onMessageReceived?.Invoke(payloadJson);

                await Task.CompletedTask;
            };

            await _mqttClient.ConnectAsync(options, CancellationToken.None);

            var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter("envmon-demo-abc123/tram01/phepdo") // Khớp topic trên Wokwi
                .Build();

            await _mqttClient.SubscribeAsync(subscribeOptions, CancellationToken.None);
        }
    }
}