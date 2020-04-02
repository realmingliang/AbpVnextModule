

class Store {
  static tokenName: string = "token";

  static tenantId: string = "tenantId";

  static languageName: string = "language";

  static SetToken(value: string) {
    this.LocalStorageSetItem(this.tokenName, value)
  }

  static GetToken(): string | null {
    return this.LocalStorageGetItem(this.tokenName)
  }

  static SetTenantId(value: string) {
    this.LocalStorageSetItem(this.tenantId, value)
  }

  static GetTenantId() {
    return this.LocalStorageGetItem(this.tenantId)
  }

  static SetLanguage(value: string) {
    this.LocalStorageSetItem(this.languageName, value)
  }

  static GetLanguage(): string | null {
    return this.LocalStorageGetItem(this.languageName)
  }

  private static LocalStorageSetItem(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  private static LocalStorageGetItem(key: string): string | null {
    return localStorage.getItem(key);
  }

  public static Clear(){
    localStorage.clear();
  }

}

export default Store
