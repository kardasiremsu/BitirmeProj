// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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
    document.getElementById("popup-form").style.display = "none";
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