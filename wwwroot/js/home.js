let currentFieldIndex = 0;
const formFields = document.querySelectorAll('#job-form > div');
const progressSteps = document.querySelectorAll('.progress-step');
const backButton = document.getElementById('back-btn');
const nextButton = document.getElementById('next-btn');
const submitButton = document.getElementById('submit-btn');



function updateProgressIndicator() {
    progressSteps.forEach((step, index) => {
        if (index === currentFieldIndex) {
            step.classList.add('active');
        } else {
            step.classList.remove('active');
        }
    });
}
function closePopup() {
    document.getElementById("overlay").style.display = "none";
    document.getElementById("popup-form").style.display = "none";
}

function closePopupDetail() {
    document.getElementById("overlay").style.display = "none";
    document.getElementById("popup-detail").style.display = "none";
}
function goToNextField() {
    if (currentFieldIndex < formFields.length - 1) {
        formFields[currentFieldIndex].style.display = 'none';
        currentFieldIndex++;
        formFields[currentFieldIndex].style.display = 'block';
    }

    updateProgressIndicator();

    backButton.style.display = 'block';
    if (currentFieldIndex === formFields.length - 1) {
        nextButton.style.display = 'none';
        submitButton.style.display = 'block';
    }
}

function goToPreviousField() {
    if (currentFieldIndex > 0) {
        formFields[currentFieldIndex].style.display = 'none';
        currentFieldIndex--;
        formFields[currentFieldIndex].style.display = 'block';
    }

    updateProgressIndicator();

    nextButton.style.display = 'block';
    if (currentFieldIndex === 0) {
        backButton.style.display = 'none';
    }
}

function openPopup() {
    document.getElementById("overlay").style.display = "block";

    currentFieldIndex = 0;
    formFields.forEach((field, index) => {
        field.style.display = index === 0 ? 'block' : 'none';
    });

    updateProgressIndicator();

    backButton.style.display = 'none';
    nextButton.style.display = 'block';
    submitButton.style.display = 'none';

    document.getElementById("popup-form").style.display = "block";
}
/*
function viewDetail(jobId) {
    //document.getElementById("overlay").style.display = "block";
    // Replace the following lines with your actual data retrieval logic from the backend
    let jobTitle, jobDescription;
    
   
    jobTitle = "Test Job " + jobId;
    jobDescription = "DescriptionTest " + jobId;


    // Update the job details in the popup
    document.getElementById("job-details").innerHTML = `
        <h2>${jobTitle}</h2>
        <p>${jobDescription}</p>
    `;

    // Display the popup
    document.getElementById("popup-detail").style.display = "block";
}

function viewDetail(jobId, jobTitle, jobDescription, jobLocation, jobType, applicationDeadline) {
       //  document.getElementById("overlay").style.display = "block";

            // Construct the detailed job information
            var details = "<p><strong>Title:</strong> " + jobTitle + "</p>" +
        "<p><strong>Description:</strong> " + jobDescription + "</p>" +
        "<p><strong>Location:</strong> " + jobLocation + "</p>" +
        "<p><strong>Type:</strong> " + jobType + "</p>" +
        "<p><strong>Application Deadline:</strong> " + applicationDeadline + "</p>";

        // Construct the HTML content of the pop-up window
        var content = "<div class='job-details'>" +
            details +
            "<button onclick='editJob(" + jobId + ")'>Edit</button>" +
            "<button onclick='applyJob(" + jobId + ")'>Apply</button>" +
            "</div>";

        // Open a pop-up window with the job details
        var popup = window.open("", "JobDetails", "width=600,height=400");
        popup.document.write(content);
        }

        function editJob(jobId) {
            // Redirect the user to the edit page with the jobId
            window.location.href = '/Job/Edit/' + jobId;
        }

        function applyJob(jobId) {
            // Add logic to handle applying to the job
            console.log('Applying to job with ID ' + jobId);
        }
    

        function viewDetail(job) {
            // Construct the detailed job information
            var details = "<p><strong>Title:</strong> " + job.jobTitle + "</p>" +
        "<p><strong>Description:</strong> " + job.jobDescription + "</p>" +
        "<p><strong>Location:</strong> " + job.jobLocation + "</p>" +
        "<p><strong>Type:</strong> " + job.jobType + "</p>" +
        "<p><strong>Application Deadline:</strong> " + job.applicationDeadline + "</p>" +
        "<p><strong>Experience Level:</strong> " + job.experienceLevel + "</p>" +
        "<p><strong>Salary:</strong> " + job.salary + "</p>";

        // Construct the HTML content of the pop-up window
        var content = "<div class='job-details'>" +
            details +
            "<button onclick='editJob(" + job.jobID + ")'>Edit</button>" +
            "<button onclick='applyJob(" + job.jobID + ")'>Apply</button>" +
            "</div>";

        // Open a pop-up window with the job details
        var popup = window.open("", "JobDetails", "width=600,height=400");
        popup.document.write(content);
        }

        function editJob(jobId) {
            // Redirect the user to the edit page with the jobId
            window.location.href = '/Job/Edit/' + jobId;
        }

        function applyJob(jobId) {
            // Add logic to handle applying to the job
            console.log('Applying to job with ID ' + jobId);
        }
  
        */

