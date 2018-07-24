Dropzone.autoDiscover = false;

function InitDropzone() {

    $('.dropzone-val').each(function (i) {

        var id = $(this).attr('id');
        var val = $(this).val();

        var options = Dropzone.options.imageDrop = {
            url: '/Images/FileUpload',
            paramName: "files",
            maxFiles: 10,
            acceptedFiles: "image/*",

            init: function () {

                imageDrop = this;

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

                console.log(idNew);

                $("#" + id).val($("#" + id).val() + "|" + idNew);

                console.log($("#" + id).val());
            }

        };

        $("div#imageDrop_" + id).dropzone(options);

    });
}