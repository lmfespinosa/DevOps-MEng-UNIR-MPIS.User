# Readme

# https://docs.microsoft.com/en-us/cli/azure/ext/eventgrid/eventgrid/event-subscription?view=azure-cli-latest#ext-eventgrid-az-eventgrid-event-subscription-create
# https://github.com/Azure/azure-cli/issues/10879#issuecomment-623794283

# Install dependencies

az extension add --name eventgrid

# Delete previous event subscriptions
# ...

# $previousSubscriptions = az eventgrid event-subscription list

# foreach ($previousSubscription in $previousSubscriptions) {

#     az eventgrid event-subscription delete --source-resource-id $topicId --name $subscriptionName --endpoint-type 'azurefunction' --endpoint $endpoint --verbose

# }


# Get event configuration

$json =  Get-Content '.\event-subscriptions.json' | ConvertFrom-Json
$total = 0
Write-Host "List created events..."
$listevents = az eventgrid event-subscription list --source-resource-id /subscriptions/$json.azure_subscription 
Write-Host $listevents

Write-Host "Creating Event Grid Subscriptions..."

foreach ($topic in $json.topics) {

    Write-Host "Topic: $($topic.id)"

    foreach ($functionApp in $topic.function_apps) {
        
        Write-Host "Function App: $($functionApp.name)"

        foreach ($subscription in $functionApp.event_subscriptions) {

            Write-Host "Subscription: $($subscription.name)"

            $endpoint = "/subscriptions/$($json.azure_subscription)/resourceGroups/$($json.resource_group)/providers/Microsoft.Web/sites/$($functionApp.name)/functions/$($subscription.function_name)"

            az eventgrid event-subscription create --source-resource-id $topic.id --name $subscription.name --endpoint-type 'azurefunction' --endpoint $endpoint --included-event-types "$($subscription.type)"  --verbose

            $total++ 
        }

    }
    
}

Write-Host "$($total) subscriptions created"