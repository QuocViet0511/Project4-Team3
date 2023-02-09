var common = {
    init: function () {
        common.registerevent();
    },
    registerevent: function () {
        $("#txtkeyword").autocomplete({
            minlength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/ListName",
                    datatype: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtkeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtkeyword").val(ui.item.label);
                return false;
            }
        })
            .autocomplete("instance")._renderitem = function (ul, item) {
                return $("<li>")
                    .append("<a>" + item.label + "</a>")
                    .appendto(ul);
            };
    }
}
common.init();

window.addEventListener('load', () => {
    const svg = '<svg class="star" id="placeholder" xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" viewBox="0 0 24 24" fill="#353535" width="18px" height="18px"><g><rect fill="none" height="24" width="24"/><path d="M14.43,10l-1.47-4.84c-0.29-0.95-1.63-0.95-1.91,0L9.57,10H5.12c-0.97,0-1.37,1.25-0.58,1.81l3.64,2.6l-1.43,4.61 c-0.29,0.93,0.79,1.68,1.56,1.09L12,17.31l3.69,2.81c0.77,0.59,1.85-0.16,1.56-1.09l-1.43-4.61l3.64-2.6 c0.79-0.57,0.39-1.81-0.58-1.81H14.43z"/></g></svg>'
    const parent = document.getElementById('ratingWrapper');
    const starAmount = 5;
    let selectedRating = 0;

    for (let i = 0; i < starAmount; i++) {
        parent.innerHTML += svg.replace('placeholder', `star${i + 1}`);
    }

    for (const star of parent.children) {
        const id = star.id.replace('star', '');

        star.addEventListener('mouseover', () => {
            for (let i = 0; i < id; i++) {
                parent.children[i].style.fill = '#ffe88c';
            }
        });

        star.addEventListener('mouseout', () => {
            for (const str of parent.children) {
                if (!str.isClicked) {
                    str.style.fill = '#353535';
                }
            }
        });

        star.addEventListener('click', () => {
            if (selectedRating === id) {
                for (const str of parent.children) {
                    str.style.fill = '#353535';
                    str.isClicked = false;
                }

                selectedRating = 0;
            } else {
                for (let i = 0; i < id; i++) {
                    parent.children[i].isClicked = true;
                }

                for (let i = id; i < parent.children.length; i++) {
                    parent.children[i].style.fill = '#353535';
                    parent.children[i].isClicked = false;
                }

                selectedRating = id;
                console.log(`your rating: ${id}/${starAmount}`);
                document.getElementById('rating').innerText = selectedRating;
            }
        });
    }
});