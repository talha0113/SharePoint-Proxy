[![Build Status](https://dev.azure.com/talha0113/Open%20Source/_apis/build/status/SharePoint-Proxy?branchName=master)]
[![Build Status](https://dev.azure.com/talha0113/Open%20Source/_apis/build/status/MicrosoftAccountProfileInformation)](https://dev.azure.com/talha0113/Open%20Source/_build/latest?definitionId=36)


## SharePoint Proxy 

**Share Online Proxy** helps redirect all the api requests to sharepoint online site, during the local front end development eith script editor webpart based approach or Add In based development. As it becomes really difficult to compile the front end application deploy test and fix.

## Usage

To start the proxy:
```sh
docker run --rm --name talha0113/sharepoint-proxy -p 8080:80 -e ASPNETCORE_ENVIRONMENT='Production' -e ProxySetting:BaseUrl='<SharePoint Site Collection Url>' -e ProxySetting:UserName='<User Name>' -e ProxySetting:Password='<Password>' -i sharepoint-proxy
```
Replace Following Paramaters
 - **<SharePoint Site Collection Url>**, example: (https://domain.sharepoint.com/sites/xyz)
 - **<User Name>**, example: (user@domain.onmicrosoft.com)
 - **<Password>** with actuall user password

After successfull start of the container browse the url `http://localhost:8080` must see front page with proxy information. also verify the the SharePoint rest api by browsing `http://localhost:8080/_api/web`
