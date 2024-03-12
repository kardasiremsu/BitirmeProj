function viewJobDetails(job) {
    var applicationDeadlineDateOnly = new Date(job.applicationDeadline).toLocaleDateString();
    // Construct the detailed job information
    var details = "<p><strong>Title:</strong> " + job.jobTitle + "</p>" +
        "<p><strong>Location:</strong> " + getJobLocationText(job.jobLocation) + "</p>" +
        "<p><strong>Type:</strong> " + getJobTypeText(job.jobType) + "</p>" +
        "<p><strong>Application Deadline:</strong> " + applicationDeadlineDateOnly + "</p>" +
        "<p><strong>Experience Level:</strong> " + getExperienceLevelText(job.experienceLevel) + "</p>" +
        "<p><strong>Salary:</strong> " + job.salary + " " + getSalaryCurrencyText(job.salaryCurrency) + "</p>";

    // Update modal content with job details
    $('#jobDetailsModal .modal-body').html(details);

    // Show the modal
    $('#jobDetailsModal').modal('show');
}

function editJob(jobId) {
    // Redirect the user to the edit page with the jobId
    window.location.href = '/Job/Edit/' + jobId;
}

function applyJob(jobId) {
    // Add logic to handle applying to the job
    console.log('Applying to job with ID ' + jobId);
}
function getJobTypeText(jobType) {

    // Replace this with your actual logic to retrieve job type text from dictionary
    var jobTypeOptions = @Html.Raw(Json.Serialize(JobApplicationOptions.JobTypeOptions));
    return jobTypeOptions[jobType] || jobType;
}
function getExperienceLevelText(experienceLevel) {
    var experienceLevelOptions = @Html.Raw(Json.Serialize(JobApplicationOptions.ExperienceLevelOptions));
    return experienceLevelOptions[experienceLevel] || experienceLevel;
}

function getWorkPlaceTypeText(workPlaceType) {
    var workPlaceTypeOptions = @Html.Raw(Json.Serialize(JobApplicationOptions.WorkPlaceTypeOptions));
    return workPlaceTypeOptions[workPlaceType] || workPlaceType;
}

function getSalaryCurrencyText(salaryCurrency) {
    var salaryCurrencyOptions = @Html.Raw(Json.Serialize(JobApplicationOptions.SalaryCurrencyOptions));
    return salaryCurrencyOptions[salaryCurrency] || salaryCurrency;
}
function getJobLocationText(jobLocation) {
    var jobLocationOptions = @Html.Raw(Json.Serialize(JobApplicationOptions.JobLocationOptions));
    return jobLocationOptions[jobLocation] || jobLocation;
}

