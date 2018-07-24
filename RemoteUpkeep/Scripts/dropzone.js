Dropzone.autoDiscover = false;

function InitDropzone() {

    $('.dropzone-val').each(function (i) {

        var id = $(this).attr('id');
        var val = $(this).val();

        //console.log(id);

        var options = Dropzone.options.imageDrop = {
            url: '/Images/FileUpload',
            paramName: "files",
            maxFiles: 10,
            acceptedFiles: "image/*",
            addRemoveLinks: true,

            init: function () {

                this.on('removedfile', function (file) {
                    var idRemoved = file.name;
                    var newVal = $("#" + id).val().replace(idRemoved, "").replace("||", "|");
                    $("#" + id).val(newVal);
                });

                if (val) {
                    var fileIds = val.split('|');
                    for (var j = 0; j < fileIds.length; j++) {
                        var fileId = fileIds[j];
                        if (fileId) {
                            var mockFile = { name: fileId, size: 12345 };
                            this.emit("addedfile", mockFile);
                            this.createThumbnailFromUrl(mockFile, "/Images/Image/" + fileId);
                            this.files[j] = mockFile;
                        }
                    }
                }
            },

            success: function (files, response) {
                var idNew = response.files[0].id;
                $("#" + id).val($("#" + id).val() + "|" + idNew);
            },
        };

        $("div#imageDrop_" + id).empty();
        $("div#imageDrop_" + id).dropzone(options);

    });
}