describe('Authentication Test', () => {
    it('should login successfully with valid credentials', () => {
        cy.visit('http://localhost:5173'); // K‰yt‰ oikeaa URL-osoitetta

        cy.get('input[placeholder="Username"]', { timeout: 10000 }).type('teppo'); // Lis‰‰ timeout
        cy.get('input[placeholder="Password"]', { timeout: 10000 }).type('testaaja'); // Lis‰‰ timeout
        cy.get('input[type="submit"]', { timeout: 10000 }).click();

        // Tarkistetaan, ett‰ tervetuloviesti n‰kyy kirjautumisen j‰lkeen
        cy.contains('Logged in as: teppo', { timeout: 10000 }).should('be.visible'); // Korvaa tervetuloviestill‰, joka n‰kyy kirjautumisen j‰lkeen
    });

    it('should navigate to customers page and display customer list after login', () => {
        cy.visit('http://localhost:5173'); // K‰yt‰ oikeaa URL-osoitetta

        cy.get('input[placeholder="Username"]', { timeout: 10000 }).type('teppo'); // Lis‰‰ timeout
        cy.get('input[placeholder="Password"]', { timeout: 10000 }).type('testaaja'); // Lis‰‰ timeout
        cy.get('input[type="submit"]', { timeout: 10000 }).click();

        // Klikataan Customers-v‰lilehte‰ kirjautumisen j‰lkeen
        cy.contains('Customers', { timeout: 10000 }).click(); // Korvaa 'Customers' oikealla v‰lilehden nimell‰

        // Klikataan Customers-nappia, joka n‰ytt‰‰ customerlistin
        cy.get('h1 nobr', { timeout: 10000 }).click(); // Korvaa 'h1 nobr' oikealla valitsimella, joka vastaa Customers-nappia
    });
});
