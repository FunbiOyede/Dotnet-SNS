# Dotnet-SNS
SNS Implementation in .NET.

## Architecture Overview
AWS SNS is an aws service used to implement messaging service between software systems and it quite different from SQS because it uses the pub/sub pattern which has multiple subscibers consuming the messages unlike sqs that only has one consumer…

#### NOTE:

- If a message is published by the SNS topic and there are no subscribers, the message published would be lost. To publish a message and for the message to be receive, A subscriber needs to be configured for SNS to publish messages or notifications. The subscriber for this project is a SQS queue.