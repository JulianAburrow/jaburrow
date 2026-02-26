// Declare the GA4 dataLayer on the Window object
interface Window {
    dataLayer: any[];
    blazorGtag: (...args: any[]) => void;
}

// Declare the global gtag() function used by GA4
declare function gtag(
    command: string,
    param1?: any,
    param2?: any
): void;