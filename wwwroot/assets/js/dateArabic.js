(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports) :
  typeof define === 'function' && define.amd ? define(['exports'], factory) :
  (global = typeof globalThis !== 'undefined' ? globalThis : global || self, factory(global.ar = {}));
}(this, (function (exports) { 'use strict';

  var fp = typeof window !== "undefined" && window.flatpickr !== undefined
      ? window.flatpickr
      : {
          l10ns: {},
      };
  var Arabic = {
      weekdays: {
          shorthand: ["أحد", "اثنين", "ثلاثاء", "أربعاء", "خميس", "جمعة", "سبت"],
          longhand: [
              "الأحد",
              "الاثنين",
              "الثلاثاء",
              "الأربعاء",
              "الخميس",
              "الجمعة",
              "السبت",
          ],
      },
      months: {
          shorthand: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
longhand: [
    "كانون الثاني",
    "شباط",
    "آذار",
    "نيسان",
    "أيار",
    "حزيران",
    "تموز",
    "آب",
    "أيلول",
    "تشرين الأول",
    "تشرين الثاني",
    "كانون الأول",
],
      },
      firstDayOfWeek: 6,
      rangeSeparator: " إلى ",
      weekAbbreviation: "Wk",
      scrollTitle: "قم بالتمرير للزيادة",
      toggleTitle: "اضغط للتبديل",
      amPM: ["ص", "م"],
      yearAriaLabel: "سنة",
      monthAriaLabel: "شهر",
      hourAriaLabel: "ساعة",
      minuteAriaLabel: "دقيقة",
      time_24hr: false,
  };
  fp.l10ns.ar = Arabic;
  var ar = fp.l10ns;

  exports.Arabic = Arabic;
  exports.default = ar;

  Object.defineProperty(exports, '__esModule', { value: true });

})));
