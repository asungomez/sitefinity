[Net.ServicePointManager]::ServerCertificateValidationCallback = {$true}
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$CollectorFile = "collector_windows64.exe"
$ExePath = -join(".\", $CollectorFile)

$WebClient = New-Object System.Net.WebClient
$WebClient.Headers['X-CS-CUSTID'] = '5e261638f86b5913bbd1b9663172285b';
try { $WebClient.DownloadFile("https://jll.app.bionic.ai:443/api/v1/resolvers/download?collectorType=64bit&collectorOS=windows",$CollectorFile) } catch { 
echo "Failed downloading the collector"
Exit 5 }

try {
  & $ExePath --% -access-token=b1b8cc2a-07d4-433f-b40f-81b208d26af3  -alternative-url=https://jll.app.bionic.ai:443/api/v1/collector  -project-id=1 -internal-cid=5e261638f86b5913bbd1b9663172285b -internal-trace-path=20f04365-46a4-44b6-90d2-fe7d2413504c/7e64a46d-9fa1-46fa-99eb-73723864c179 -internal-scope='{"OrganizationId":1,"ProjectId":1,"TracePath":"20f04365-46a4-44b6-90d2-fe7d2413504c/7e64a46d-9fa1-46fa-99eb-73723864c179","TaskUUID":"38f2f24d-c221-40f0-8550-2b135db6705b"}' -internal-associated-id=04cdd23f-38fd-4643-aa6e-00d1b5743d95 -internal-collection-type=INTEGRATION_TASK_TYPE_AZURE --require-memory-guard=false  
} 
catch { Exit 7 }
