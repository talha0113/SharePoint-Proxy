[![Board Status](https://dev.azure.com/talha0113/3504235c-4145-42ee-9d73-6f794f3258fd/c2ec2762-6a03-4766-903d-a7d583fc9d09/_apis/work/boardbadge/2869cebf-e0ac-4182-9b0c-0e4dab9014ec)](https://dev.azure.com/talha0113/3504235c-4145-42ee-9d73-6f794f3258fd/_boards/board/t/c2ec2762-6a03-4766-903d-a7d583fc9d09/Microsoft.RequirementCategory)
[![Build Status](https://dev.azure.com/talha0113/Open%20Source/_apis/build/status/SharePoint-Proxy?branchName=master)](https://dev.azure.com/talha0113/Open%20Source/_build/latest?definitionId=44&branchName=master)

## SharePoint Proxy 

**Share Online Proxy** helps redirect all the api requests to sharepoint online site, during the local front end development with script editor webpart based approach or Add In based development. As it becomes really difficult to compile the front end application deploy test and fix.

## Usage

To start the proxy:
``
docker run --rm --name sharepoint-proxy-container -p 8080:80 -e ASPNETCORE_ENVIRONMENT='Production' -e ProxySetting:BaseUrl='<SharePoint Site Collection Url>' -e ProxySetting:UserName='<User Name>' -e ProxySetting:Password='<Password>' -i talha0113/sharepoint-proxy
``
Replace Following Paramaters
 - **`<SharePoint Site Collection Url>`**, example: (https://domain.sharepoint.com/sites/xyz)
 - **`<User Name>`**, example: (user@domain.onmicrosoft.com)
 - **`<Password>`** with actuall user password

After successfull start of the container browse the url `http://localhost:8080` must see front page with proxy information. also verify the the SharePoint rest api by browsing `http://localhost:8080/_api/web`
