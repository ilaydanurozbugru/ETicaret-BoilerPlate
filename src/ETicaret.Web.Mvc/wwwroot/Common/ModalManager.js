
$.fn.buttonBusy = function (isBusy) {
    return $(this).each(function () {
        var $button = $(this);
        var $icon = $button.find('i');
        var $buttonInnerSpan = $button.find('span');

        if (isBusy) {
            if ($button.hasClass('button-busy')) {
                return;
            }

            $button.attr('disabled', 'disabled');

            //change icon
            if ($icon.length) {
                $button.data('iconOriginalClasses', $icon.attr('class'));
                $icon.removeClass();
                $icon.addClass('fa fa-spin fa-spinner');
            }

            //change text
            if ($buttonInnerSpan.length && $button.attr('busy-text')) {
                $button.data('buttonOriginalText', $buttonInnerSpan.html());
                $buttonInnerSpan.html($button.attr('busy-text'));
            }

            $button.addClass('button-busy');
        } else {
            if (!$button.hasClass('button-busy')) {
                return;
            }

            //enable button
            $button.removeAttr('disabled');

            //restore icon
            if ($icon.length && $button.data('iconOriginalClasses')) {
                $icon.removeClass();
                $icon.addClass($button.data('iconOriginalClasses'));
            }

            //restore text
            if ($buttonInnerSpan.length && $button.data('buttonOriginalText')) {
                $buttonInnerSpan.html($button.data('buttonOriginalText'));
            }

            $button.removeClass('button-busy');
        }
    });
};

var app = app || {};
(function ($) {

    app.modals = app.modals || {};

    app.ModalManager = (function () {

        var _normalizeOptions = function (options) {
            if (!options.modalId) {
                options.modalId = 'Modal_' + (Math.floor((Math.random() * 1000000))) + new Date().getTime();
            }

            if (options.modalSize === null) {
                options.modalSize = "";
            }
            else if (options.modalSize) {
                options.modalSize = options.modalSize;
            } else {
                options.modalSize = 'modal-lg';
            }
        };

        function _removeContainer(modalId) {
            var _containerId = modalId + 'Container';
            var _containerSelector = '#' + _containerId;

            var $container = $(_containerSelector);
            if ($container.length) {
                $container.remove();
            }
        }

        function _createContainer(modalId, modalSize, cssClass) {
            _removeContainer(modalId);

            var _containerId = modalId + 'Container';
            return $('<div   id="' + _containerId + '"></div>')
                .append(
                    '<div id="' + modalId + '" class="modal fade ' + cssClass + '" tabindex="-1" role="modal" aria-hidden="true">' +
                    '  <div class="modal-dialog ' + modalSize + '">' +
                    '    <div class="modal-content"></div>' +
                    '  </div>' +
                    '</div>'
                ).appendTo('body');
        }

        return function (options) {

            _normalizeOptions(options);

            var _options = options;
            var _$modal = null;
            var _modalId = options.modalId;
            var _modalSelector = '#' + _modalId;
            var _modalObject = null;
            var _modalSize = options.modalSize;
            var _modalcssClass = options.cssClass;
            var _publicApi = null;
            var _args = null;
            var _getResultCallback = null;
            var _onShownCallback = null;

            var _onCloseCallbacks = [];
            var _onBeforeCloseCallbacks = [];

            function _saveModal() {
                if (_modalObject && _modalObject.save) {
                    _modalObject.save();
                }
            }

            function _initAndShowModal() {
                _$modal = $(_modalSelector);

                _$modal.modal({
                    backdrop: 'static'
                });

                _$modal.on('hidden.bs.modal', function () {
                    for (var i = 0; i < _onBeforeCloseCallbacks.length; i++) {
                        _onBeforeCloseCallbacks[i]();
                    }

                    _removeContainer(_modalId);

                    for (var i = 0; i < _onCloseCallbacks.length; i++) {
                        _onCloseCallbacks[i]();
                    }

                    if (typeof _options.removeAllOnCloseBindsAfterModalClose == "boolean" && _options.removeAllOnCloseBindsAfterModalClose) {
                        _onCloseCallbacks = [];
                        _onBeforeCloseCallbacks = [];
                    }
                });

                _$modal.on('shown.bs.modal', function () {
                    _$modal.find('input:not([type=hidden]):first').focus();
                    if (_onShownCallback) {
                        onShownCallback($modal);
                    }
                });

                var modalClass = app.modals[options.modalClass];

                if (modalClass) {
                    _modalObject = new modalClass();
                    if (_modalObject.init) {
                        _modalObject.init(_publicApi, _args);
                    }
                }

                _$modal.find('.save-button').click(function () {
                    _saveModal();
                });

                _$modal.find('.modal-body').keydown(function (e) {
                    if (e.which === 13) {
                        if (e.target.tagName.toLocaleLowerCase() === "textarea" || e.target.className === "note-editable") {
                            e.stopPropagation();
                        } else {
                            e.preventDefault();
                            _saveModal();
                        }

                    }
                });

                _$modal.modal('show');
            }

            var _open = function (args, getResultCallback, onShownCallback) {

                _args = args || {};
                _getResultCallback = getResultCallback;
                _onShownCallback = onShownCallback;

                abp.ui.setBusy($("body"));

                _createContainer(_modalId, _modalSize, _modalcssClass)
                    .find('.modal-content')
                    .load(options.viewUrl, _args, function (response, status, xhr) {
                        if (status == "error") {
                            abp.message.warn(abp.localization.abpWeb('InternalServerError'));
                            return;
                        };

                        if (options.scriptUrl) {
                            app.ResourceLoader.loadScript(options.scriptUrl, function () {
                                _initAndShowModal();
                            });
                        } else {
                            _initAndShowModal();
                        }

                        abp.ui.clearBusy($("body"));
                    });
            };

            var _close = function () {
                if (!_$modal) {
                    return;
                }

                _$modal.modal('hide');
            };

            var _onClose = function (onCloseCallback) {
                _onCloseCallbacks.push(onCloseCallback);
            };

            var _onBeforeClose = function (onBeforeCloseCallback) {
                _onBeforeCloseCallbacks.push(onBeforeCloseCallback);
            };

            function _setBusy(isBusy) {
                if (!_$modal) {
                    return;
                }

                _$modal.find('.modal-footer button').buttonBusy(isBusy);
                _$modal.find('.modal-header button.close').buttonBusy(isBusy);
            }

            _publicApi = {
                open: _open,

                reopen: function () {
                    _open(_args);
                },

                close: _close,

                getModalId: function () {
                    return _modalId;
                },

                getModal: function () {
                    return _$modal;
                },

                getArgs: function () {
                    return _args;
                },

                getOptions: function () {
                    return _options;
                },

                setBusy: _setBusy,

                setResult: function () {
                    _getResultCallback && _getResultCallback.apply(_publicApi, arguments);
                },

                onClose: _onClose,

                getModalObject: function () {
                    return _modalObject
                },

                onBeforeClose: _onBeforeClose
            };

            return _publicApi;
        };
    })();

})(jQuery);