function validateUserInput(firstName, lastName) {
    if (!firstName || !lastName) {
        alert("First name and last name are required.");
        return false;
    }

    if (!isValidName(firstName) || !isValidName(lastName)) {
        alert("Names must only contain alphabetic characters.");
        return false;
    }

    if (!isUniqueFullName(firstName, lastName)) {
        alert("A contact with the same full name already exists.");
        return false;
    }

    return true;
}

function isValidName(name) {
    return /^[A-Za-z]+$/.test(name);
}

function isUniqueFullName(firstName, lastName) {
    // Simulate a check for unique full name
    // In a real scenario, this would involve an API call to the server
    const existingContacts = [
        { firstName: "John", lastName: "Doe" },
        { firstName: "Jane", lastName: "Smith" }
    ];

    return !existingContacts.some(contact => contact.firstName === firstName && contact.lastName === lastName);
}

// Example usage
document.getElementById("submitButton").addEventListener("click", function() {
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;

    if (validateUserInput(firstName, lastName)) {
        alert("User input is valid.");
        // Proceed with form submission or other actions
    }
});
