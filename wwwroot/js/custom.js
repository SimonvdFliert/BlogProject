let index = 0;

function AddTag() {
    //Get a reference to the TagEntry input element
    var tagEntry = document.getElementById("TagEntry");

    //Implement the search function below to help detect an error state
    let searchResult = Search(tagEntry.value);
    if (searchResult != null) {
        // Trigger my sweet alert for the error (the searchResult var)
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult} </span>`

        });
    }
    else {
        //Create a new Select Option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;

    }
    //Clear out the TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList");
    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'> Please choose a tag before deleting </span>"
        })
    }

    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else {
            tagCount = 0;
            --index;
        }
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

// Loog for the tagValues variable to see if it has data
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (var loop = 0; loop < tagArray.length; loop++) {
        //Load up our replace the options that we have
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}

//The Search function will detect either an empty or a duplicate Tag
// and return an error string if an error is detected
function Search(str) {
    if (str == "") {
        return 'Empty tags are not permitted';
    }

    var tagsEl = document.getElementById("TagList");

    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str) {
                return `The Tag #${str} is a duplicate and is not permitted`
            }
        }
    }
}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-block btn-outline-dark'
    },
    imageUrl: '/assets/img/error.png',
    timer: 3000,
    buttonsStyling: false
});