using System;
using System.Linq;
using System.Threading.Tasks;

using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace SnsSendMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Aqui un mensaje" + DateTime.Now.ToShortTimeString();
            string sTopicArn = "arn:aws:sns:us-east-1:908548507979:TopicSecureCloud";

            Amazon.Runtime.AWSCredentials myCredentials;
            var chain = new Amazon.Runtime.CredentialManagement.CredentialProfileStoreChain();
            if (chain.TryGetAWSCredentials("awsvisualstudio", out myCredentials)){
                var client = new AmazonSimpleNotificationServiceClient(myCredentials,Amazon.RegionEndpoint.USEast1);
                var requestsub = new SubscribeRequest(sTopicArn,"sms","+15146382105");

                var responsesub = client.Subscribe(requestsub);

                var request = new PublishRequest
                {
                    Message = message,
                    TopicArn = sTopicArn                };

                try
                {
                    var response = client.Publish(request);

                    Console.WriteLine("Message sent to topic:");
                    Console.WriteLine(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Caught exception publishing request:");
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}

