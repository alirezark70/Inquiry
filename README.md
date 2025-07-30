<div dir="rtl" align="right">

# معرفی پروژه

پروژه **استعلام** با هدف تمرین و یادگیری مفاهیم مقاوم‌سازی (Resilience) در سرویس‌ها و برنامه‌های تحت دات‌نت ساخته شده است.  
در این پروژه با استفاده از کتابخانه‌ی **Polly**، سناریوهای مختلفی مانند سیاست‌های تلاش مجدد (**Retry Policy**), (**Circuit Breaker**)، محدودیت زمانی (**Timeout**) و سایر روش‌های مدیریت خطا و استثنا پیاده‌سازی شده‌اند.

---

## هدف پروژه

- آشنایی با نحوه‌ی استفاده از **Polly** در برنامه‌های دات‌نت  
- پیاده‌سازی سیاست‌های مختلف مقاوم‌سازی مانند:
    - تلاش مجدد (**Retry**)
    - مدارشکن (**Circuit Breaker**)
    - محدودیت زمانی (**Timeout**)
- تست رفتار سرویس‌ها در سناریوهای مختلف خطا  
- یادگیری عملی مفاهیم **Fault Tolerance** و **Resiliency**

---

## تکنولوژی‌های استفاده شده

 .NET (نسخه مورد استفاده: 9)
 
 Polly

 MediatR

---

</div>


<div dir="rtl" align="right">

## ویژگی‌های کلیدی

۱. **معماری لایه‌ای مناسب**  
  - Domain Layer: مدل‌های پایه و Enumها  
  - Application Layer: Interfaceها، Exceptionها و Behaviorها  
  - Infrastructure Layer: پیاده‌سازی Serviceها  
  - WebApi Layer: Filterها، Middleware و Controllerها  


۲. **Wrapping خودکار**  

  - با استفاده از ApiResponseActionFilter همه پاسخ‌ها به صورت خودکار wrap می‌شوند  
  - نیازی به تغییر در Controller های موجود نیست  

۳. **مدیریت جامع خطاها**  
  - GlobalExceptionFilter برای handle کردن همه Exceptionها  
  - پشتیبانی از انواع مختلف خطا (Validation, NotFound, Unauthorized, etc.)  
  - نمایش Stack Trace فقط در محیط Development  

۴. **پشتیبانی از صفحه‌بندی**  
  - مدل PagedResponse برای نمایش داده‌های صفحه‌بندی شده  
  - اطلاعات کامل صفحه‌بندی در Meta  

۵. **سازگاری با MediatR**  
  - ValidationBehavior برای اعتبارسنجی خودکار  
  - پشتیبانی از FluentValidation  

۶. **قابلیت ردیابی**  
  - هر Response دارای TraceId منحصر به فرد  
  - Timestamp برای زمان پاسخ  

</div>

<div dir="rtl" align="right">

## ویژگی‌های این پیاده‌سازی

۱. **Policy Registry Pattern**
    - مدیریت متمرکز تمام سیاست‌های Polly  
    - قابلیت استفاده‌ی مجدد از سیاست‌ها  
۲. **Decorator Pattern** با `ResilientPersonInquiryService`  
    - جداسازی منطق Resilience از منطق اصلی  
    - امکان تست آسان‌تر  
۳. **سیاست‌های پیاده‌سازی شده:**  
    - تلاش مجدد با استراتژی **Exponential Backoff**
    - مدارشکن: محافظت در برابر سرویس‌های معیوب  
    - محدودیت زمانی برای درخواست‌ها  
    - ارائه‌ی پاسخ جایگزین (**Fallback**) در صورت بروز خطا  
۴. **قابلیت‌های اضافی:**  
    - اعتبارسنجی کد ملی  
    - لاگ‌گذاری جامع  
    - پیکربندی از طریق `appsettings.json`  

---

## نحوه اجرا

۱. پروژه را Clone کنید  
۲. بسته‌های مورد نیاز (Polly و ...) را نصب نمایید  
۳. پروژه را اجرا و سیاست‌های مختلف را تست کنید  

</div>