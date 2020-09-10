// adapted from OrchardCore source
const puppeteer = require('puppeteer');
const orchard = require('./orchard.js');

let browser;
let page;
let basePath;
let error;

// path to startup web project assembly
const webProjectPath = '../../../LETS.Web/';
const webProjectAssembly = 'LETS.Web.dll';

// e.g., npm test --debug
// In debug mode we show the editor, slow down operations, and increase the timeout for each test
let debug = process.env.npm_config_debug || false;
jest.setTimeout(debug ? 60000 : 30000);

beforeAll(async () => {
    try {
        basePath = orchard.run(webProjectPath, webProjectAssembly, true);
        browser = await puppeteer.launch(debug ? { headless: false, slowMo: 100 } : {});
        page = await browser.newPage();
    } catch (ex) {
        error = ex;
    }
});

afterAll(async () => {
    if (browser) {
        await browser.close();
    }

    orchard.stop();
    orchard.cleanAppData(webProjectPath);
    orchard.printLog();
});

describe('Browser is initialized', () => {
    test('no errors on launch', () => {
        expect(error).toBeUndefined();

        // Sanity testing
        expect(basePath).toBeDefined();
        expect(browser).toBeDefined();
        expect(page).toBeDefined();
    })
})

describe('Setup', () => {

    beforeAll(async () => {
        await page.goto(`${basePath}`);
    });

    it('should display "Orchard Setup"', async () => {
        await expect(await page.content()).toMatch('Orchard Setup');
    });

    it('should offer LETS recipe', async () => {
        await expect(await page.content()).toMatch('Orchard Core website with LETS module enabled');
    });

    it('should setup a LETS site with 3 built in NoticeTypes', async () => {
        await page.type('#SiteName', 'LETS site');
        await page.click('#recipeButton');
        await (await page.$x("//a[contains(text(), 'LETS')]"))[0].click();
        await page.select('#DatabaseProvider', 'Sqlite');
        await page.type('#TablePrefix', '');
        await page.type('#UserName', 'admin');
        await page.type('#Email', 'admin@example.com');
        await page.type('#Password', 'Demo123!');
        await page.type('#PasswordConfirmation', 'Demo123!');

        await Promise.all([
            page.waitForNavigation(),
            page.click('#SubmitButton'),
        ]);

        await expect(await page.content()).toMatch('Welcome to the Orchard Framework, your site has been successfully set up');

        // frontend
        
        await page.goto(`${basePath}/noticetypes/offer`);
        await expect(await page.content()).toMatch('Offer');
        await page.goto(`${basePath}/noticetypes/request`);
        await expect(await page.content()).toMatch('Request');
        await page.goto(`${basePath}/noticetypes/announcement`);
        await expect(await page.content()).toMatch('Announcement');

        // backend
        await page.goto(`${basePath}/admin`);
        await page.type('#UserName', 'admin');
        await page.type('#Password', 'Demo123!');
        await Promise.all([
            page.waitForNavigation(),
            page.keyboard.press('Enter')
        ]);
        await expect(await page.url()).toBe(`${basePath}/admin`);
        await page.goto(`${basePath}/Admin/Contents/ContentItems/NoticeType`);
        await expect(await page.content()).toMatch('Offer');
        await expect(await page.content()).toMatch('Request');
        await expect(await page.content()).toMatch('Announcement');
    });

    it('should setup a first locality', async () => {
        // frontend
        await page.goto(`${basePath}/localities/first-locality`);
        await expect(await page.content()).toMatch('First locality');

        // backend
        await page.goto(`${basePath}/Admin/Contents/ContentItems/Locality`);
        await expect(await page.content()).toMatch('First locality');
    });

    it('should setup initial categories', async () => {
        await page.goto(`${basePath}/categories/buy-sell/art-craft-supplies`);
        await expect(await page.content()).toMatch('Art &amp; craft supplies');
    });

});

