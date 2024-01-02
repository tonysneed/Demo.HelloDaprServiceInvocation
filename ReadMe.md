# Dapr Service Invocation Demo

This is a demo project for using Dapr service invocation on macOS when things like anti-virus may interfere with address resolution.

Here is the relevant GitHub issue: https://github.com/dapr/dapr/issues/7323

## Usage

1. At API project root run:

   ```
   dapr run --app-id api --app-port 5096 --log-level debug --enable-api-logging -- dotnet run
   ```
2. At Console project root run: 
   
   ```
   dapr run --app-id app --log-level debug --enable-api-logging -- dotnet run
   ```

## Behavior

`DaprClient.InvokeMethodAsync` returns:

```
Response status code does not indicate success: 500 (Internal Server Error)
``````

## Resolution

1. Install Consul: https://developer.hashicorp.com/consul/install
2. Start Consul: `consul agent -dev`
3. Update `~/.dapr/config.yaml` file:

```yaml
apiVersion: dapr.io/v1alpha1
kind: Configuration
metadata:
  name: daprConfig
spec:
  nameResolution:
    component: "consul"
    configuration:
      selfRegister: true
  tracing:
    samplingRate: "1"
    zipkin:
      endpointAddress: http://localhost:9411/api/v2/spans
```
