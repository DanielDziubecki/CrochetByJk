var AddNewProduct = React.createClass({
    getInitialState: function () {
        return {formDisabled: false, showGoToProductButton: false, productUrl: null}
    },
    render: function () {
        return (this.state.showGoToProductButton
            ? <GoToNewProduct url={this.state.productUrl}/>
            : <ReactCSSTransitionGroup
                transitionName="adminFormAnimation"
                transitionAppear={true}
                transitionAppearTimeout={500}>
                <div id="addNewProductForm">
                    <ProductForm
                        categories={this.props.categories}
                        formDisabled={this.state.formDisabled}
                        submitForm={this.submitForm}/>
                </div>
            </ReactCSSTransitionGroup >);
    },
    submitForm: function (e) {
        var form = $("#productForm");
        form.validate({
            errorPlacement: function (error, element) {
                if (element.attr("name") == "mainImage") {
                    error.insertAfter("#mainImageError");
                } else if (element.attr("name") == "galleryImages") {
                    error.insertAfter("#galleryImagesError");
                } else {
                    error.insertAfter(element);
                }
            }
        });
        if (!form.valid()) 
            return;
        var self = this;
        e.preventDefault();
        $('#newProduct').text("");
        $('#newProduct').append('Dodajemy produkt..&nbsp &nbsp<i class="fa fa-refresh fa-spin"></i>');
        self.setState({formDisabled: true})
        var data = new FormData();
        data.append("Name", $("#newProductName").val())
        data.append("Description", $("#productDescription").val())
        data.append("GalleryUri", $("#productGalleryImages").val())

        jQuery.each(jQuery('#gallery-images')[0].files, function (i, file) {
            data.append('gallery-image-' + i, file);
        });

        data.append('MainPhoto', $('#main-image')[0].files[0]);
        data.append('IdCategory', $("#productCategory option:selected").val());
        data.append('CategoryName', $("#productCategory option:selected").text());

        $.ajax({
            url: 'produkty/dodajnowy',
            type: 'post',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result.Success == "False") {
                    $('#validationMsg').text(result.responseText);
                    $('#newProduct').text("Dodaj nowy produkt");
                    return;
                }
                self.setState({productUrl: result.Url, showGoToProductButton: true})
            }
        });
        self.setState({formDisabled: false});
    }
});

var GoToNewProduct = ({url}) => (
    <div id="goToNewProduct">
        <ReactCSSTransitionGroup
            transitionName="adminFormAnimation"
            transitionAppear={true}
            transitionAppearTimeout={500}>
            <Button className="adminButton" href={url}>
                Przejdź do produktu
            </Button>
        </ReactCSSTransitionGroup>
    </div>
);