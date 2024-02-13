const http = require('http')
const port = 4300

const jsonData = {    
    "actions": [],    
    "displayName": "It Works PR-723",    
    "fullDisplayName": "mockProj/PR-723",
    "fullName": "mockProj/PR-723",
    "latestRun": {
        "actions": [],
        "artifactsZipFile": "artifacts url",
        "endTime": "2024-02-13T09:02:58.853-0500",
        "id": "13",
        "organization": "jenkins",
        "pipeline": "PR-723",
        "result": "SUCCESS",
        "runSummary": "back to normal",
        "startTime": "2024-02-13T08:44:27.374-0500",
        "state": "FINISHED",
        "type": "WorkflowRun",
        "changeSet": []
    },
    "name": "PR-723",
    "organization": "jenkins",    
    "weatherScore": 60,
    "branch": {
        "isPrimary": false,
        "issues": [],
        "url": "https://ghe.mockorg.net/orgs/mockProj/pull/723"
    },
    "pullRequest": {
        "author": "asuresh",
        "id": "723",
        "title": "MG-1416: Adding save and more options to header",
        "url": "https://ghe.mockorg.net/orgs/mockProj/pull/723"
    }
};

// Create a server object:
const server = http.createServer(function (req, res) {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET');
    
	res.writeHead(200, { "Content-Type": "application/json" });
	res.write(JSON.stringify(jsonData));
    res.end();
})

// Set up our server so it will listen on the port
server.listen(port, function (error) {

	// Checking any error occur while listening on port
	if (error) {
		console.log('Something went wrong', error);
	}
	// Else sent message of listening
	else {
		console.log('Server is listening on port' + port);
	}
})
