const firstname = document.getElementById('firstname')
const middlname = document.getElementById('middlename')
const lastname = document.getElementById('lastname')

const birthtown = document.getElementById('birthtown')
const shortbio = document.getElementById('shortbio')

const form = document.getElementById('addnpc')
const npcadderrorElement = document.getElementById('npcadderror')

form.addEventListener('submit', (e) => {
    let errorMessages = []
    if (firstname.nodeValue == "" || firstname.nodeValue == null) {
        errorMessages.push("A First Name is required")
    }

    if (middlename.length > 20 || firstname.length > 20 || lastname.lastname > 20) {
        errorMessages.push("Names must contain less than 20 characters each.")
    }

    if (image == null) {
        errorMessages.push("An image is required")
    }

    if (shortbio.length > 100) {
        errorMessages.push("The Short Biography must contain at most 100 characters.")
    }

    if (messages.length > 0) {
        e.preventDefault()
        npcadderrorElement.innerText = messages.join(', ')
    }
})