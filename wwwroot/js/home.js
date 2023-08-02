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

function viewDetail(jobId) {
    //document.getElementById("overlay").style.display = "block";
    // Replace the following lines with your actual data retrieval logic from the backend
    let jobTitle, jobDescription;
    if (jobId === 1) {
        jobTitle = "Job Title 1";
        jobDescription = "This is the description for Job Title 1.";
    } else if (jobId === 2) {
        jobTitle = "Job Title 2";
        jobDescription = "This is the description for Job Title 2.";
    }

    // Update the job details in the popup
    document.getElementById("job-details").innerHTML = `
        <h2>${jobTitle}</h2>
        <p>${jobDescription}</p>
    `;

    // Display the popup
    document.getElementById("popup-detail").style.display = "block";
}