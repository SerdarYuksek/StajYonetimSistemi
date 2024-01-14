// image.js

// Sample data for image list
let imageListData = [
    { id: 1, name: 'Image 1.jpg' },
    { id: 2, name: 'Image 2.png' },
    { id: 3, name: 'Image 3.jpeg' },
];

// Function to initialize the image list
function initImageList() {
    const imageListElement = document.getElementById('imageList');
    imageListData.forEach(image => {
        const listItem = document.createElement('li');
        listItem.textContent = image.name;
        imageListElement.appendChild(listItem);
    });

    // Initialize delete image select options
    updateDeleteImageSelect();
}

// Function to update the delete image select options
function updateDeleteImageSelect() {
    const deleteImageSelect = document.getElementById('deleteImageSelect');
    deleteImageSelect.innerHTML = '';

    imageListData.forEach(image => {
        const option = document.createElement('option');
        option.value = image.id;
        option.textContent = image.name;
        deleteImageSelect.appendChild(option);
    });
}

// Function to save an image
function saveImage() {
    alert('Image saved!');
    // Implement your save image logic here
}

// Function to update an image
function updateImage() {
    alert('Image updated!');
    // Implement your update image logic here
}

// Function to delete an image
function deleteImage() {
    const selectedImageId = document.getElementById('deleteImageSelect').value;
    const index = imageListData.findIndex(image => image.id === parseInt(selectedImageId));

    if (index !== -1) {
        // Remove the image from the list
        imageListData.splice(index, 1);

        // Update the image list on the page
        updateImageList();
        updateDeleteImageSelect();

        alert(`Image with ID ${selectedImageId} deleted!`);
    } else {
        alert('Image not found!');
    }

    // Implement your delete image logic here
}

// Function to update the image list on the page
function updateImageList() {
    const imageListElement = document.getElementById('imageList');
    imageListElement.innerHTML = '';

    imageListData.forEach(image => {
        const listItem = document.createElement('li');
        listItem.textContent = image.name;
        imageListElement.appendChild(listItem);
    });
}

// Function to handle file upload and update the list
function dosyaYukle() {
    const fileInput = document.getElementById('imageInput');
    const fileName = fileInput.value.split('\\').pop(); // Get the file name from the path

    // Check if the file name is not empty
    if (fileName) {
        // Generate a unique ID for the new image
        const newImageId = imageListData.length + 1;

        // Add the new image to the list
        imageListData.push({ id: newImageId, name: fileName });

        // Update the image list on the page
        updateImageList();
        updateDeleteImageSelect();

        alert(`Image ${fileName} uploaded!`);
    } else {
        alert('Please select a file to upload.');
    }
}

// Initialize the image list when the page loads
document.addEventListener('DOMContentLoaded', initImageList);
