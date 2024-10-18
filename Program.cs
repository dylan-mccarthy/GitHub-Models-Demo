using Azure;
using Azure.AI.Inference;

// To authenticate with the model you will need to generate a personal access token (PAT) in your GitHub settings. 
// Create your PAT token by following instructions here: https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens
var token = System.Environment.GetEnvironmentVariable("GITHUB_TOKEN");
if (string.IsNullOrEmpty(token))
{
    throw new System.ArgumentNullException("GITHUB_TOKEN", "Please set the GITHUB_TOKEN environment variable.");
}

var credential = new AzureKeyCredential(token);

var url = new Uri("https://models.inference.ai.azure.com");
var client = new ChatCompletionsClient(
    url,
    credential,
    new ChatCompletionsClientOptions());

var requestOptions = new ChatCompletionsOptions()
{
    Messages =
    {
        new ChatRequestSystemMessage(""),
        new ChatRequestUserMessage("Can you explain the basics of machine learning?"),
    },
    Model = "Phi-3.5-mini-instruct",
    Temperature = 0,
    MaxTokens = 4096,

};

Response<ChatCompletions> response = client.Complete(requestOptions);
System.Console.WriteLine(response.Value.Choices[0].Message.Content);
